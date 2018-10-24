using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {
    private GameObject target;
    private NavMeshAgent agent;
    private Health health;
    private Animator animator;
    private Collider collider;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isAttacking = false;
    public float speed = 1f;
    public float angularSpeed = 120;
    public float damage = 20;
    private Health targetHealth;
    private Player player;
    [HideInInspector] public UnityEvent onDead;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetHealth = target.GetComponent<Health>();
        if (targetHealth == null)
        {
            throw new System.Exception("Could not find player health");
        }

        player = target.GetComponent<Player>();
        if (player == null)
        {
            throw new System.Exception("Player doesnt have player component");
        }
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }

    void Update()
    {
        CheckHealth();
        Chase();
        CheckAttack();
    }

    void CheckHealth(){
        if (isDead) return;
        if(health.value <= 0){
            onDead.Invoke();
            isDead = true;
            agent.isStopped = true;
            collider.enabled = false;
            animator.CrossFadeInFixedTime("Death", 0.1f);
            Destroy(gameObject, 3f);
        }
    }

    void Chase(){
        if (isDead) return;
        else if (player.isDead) return;
        agent.destination = target.transform.position;

    }

    void CheckAttack(){
        if (isDead) return;
        else if (isAttacking) return;
        else if (player.isDead) return;
        float distanceFromTarget = Vector3.Distance(target.transform.position, transform.position);

        if(distanceFromTarget <= 1.8f){
            Attack();
        }


    }

    void Attack(){
       
        targetHealth.TakeDamage(damage);
        agent.speed = 0;
        agent.angularSpeed = 0;
        isAttacking = true;
        animator.SetTrigger("ShouldAttack");

        Invoke("ResetAttacking", 1.5f);
    }

    void ResetAttacking()
    {
        isAttacking = false;
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
    }
}
