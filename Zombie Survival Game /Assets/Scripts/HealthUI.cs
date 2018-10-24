using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    private Health health;
    public Text healthText;

    void Start()
    {
        Transform inGameUITransform = GameObject.Find("/Canvas/InGame").transform;
        healthText = inGameUITransform.Find("Health").GetComponent<Text>();
        health = GetComponent<Health>();
        health.onHit.AddListener(updateHealthText);
        updateHealthText();
    }

    void updateHealthText(){
        healthText.text = "HP: " + health.value;
    }

    void Update()
    {
        healthText.text = "HP: " + health.value;
    }


}
