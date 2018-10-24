using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Health health;
    [HideInInspector] public bool isDead = false;
    [SerializeField] private Animator deathAnimator;
    public bool speedBoostPowerupBool = false;
    public int speedBoostTimer = 5;
    public int currentTime;

  

    void Start()
    {
        Transform inGameUITransform = GameObject.Find("/Canvas/InGame").transform;
        deathAnimator = inGameUITransform.Find("Death").GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "coin_pickup(Clone)")
        {
            Destroy(other.gameObject);
            CashSystem.m_Cash += 50f;
            print("Score" + CashSystem.m_Cash);
            GameManager.maxPowerups--;
            GameManager.doubleScoreCount--;

        }

        if (other.gameObject.name == "lightningBolt_pickup(Clone)")
        {
            speedBoostPowerupBool = true;
            currentTime = (int)Time.time;
            Destroy(other.gameObject);
            GetComponent<Rigidbody>().velocity = new Vector3(30, 30, 30);

        }

        if (other.gameObject.name == "healthPlus_pickup(Clone)")
        {
            Destroy(other.gameObject);
            if(health.value < 100){
                health.value += 20;
            }
            GameManager.maxPowerups--;
            GameManager.healthCount--;
        }

        if (other.gameObject.name == "potion_pickup_bottle(Clone)")
        {
            Destroy(other.gameObject);
            if(health.value < 100){
                health.value = 100;
            }
            GameManager.maxPowerups--;
            GameManager.maxHealthCount--;
        }
    }

    void Update()
    {
        CheckHealth();
        if(Time.time >= currentTime + speedBoostTimer && speedBoostPowerupBool){
            speedBoostPowerupBool = false;
            GetComponent<Rigidbody>().velocity = new Vector3(5, 5, 5);
        }
    }

    void CheckHealth()
    {
        if (isDead) return;
        if (health.value <= 0)
        {
            isDead = true;


            deathAnimator.SetTrigger("Show");

            GameManager.instance.GameOver();
            Invoke("RestartGame", 1);
        }
    }
    void RestartGame(){
        deathAnimator.SetTrigger("Reset");
        GameManager.instance.ResetGame();
        Destroy(gameObject);
    }
}
