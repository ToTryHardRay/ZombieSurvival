using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashSystem : MonoBehaviour {
    public static float m_Cash;
    public Text cashText;
    public int initialCash = 0;
    public float cash{
        get{
            return m_Cash;
        }
        set{
            m_Cash = value;
            //updateUI();
        }
    }
	// Use this for initialization
	void Start () {
        Transform inGameUITransform = GameObject.Find("/Canvas/InGame").transform;
        cashText = inGameUITransform.Find("Cash").GetComponent<Text>();
        cash = initialCash;
	}
	
	// Update is called once per frame
	void Update() {
        cashText.text = "Score: " + m_Cash;
	}
}
