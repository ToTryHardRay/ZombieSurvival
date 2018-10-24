using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float value = 100f;
    [HideInInspector] public UnityEvent onHit;

    public void TakeDamage(float damage){
        value -= damage;

        onHit.Invoke();

        if(value < 0){
            value = 0;
        }
    }
}
