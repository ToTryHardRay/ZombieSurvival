﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon{
    Police9mm,
    PortableMagnum,
    Compact9mm,
    UMP45,
    StovRifle
}

public class WeaponManager : MonoBehaviour {
    public static WeaponManager instance;
    private int currentWeaponIndex = 0;
    private Weapon[] weapons = { 
        Weapon.Police9mm, 
        Weapon.PortableMagnum, 
        Weapon.Compact9mm, 
        Weapon.UMP45, 
        Weapon.StovRifle};

    void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SwitchToCurrentWeapon();
    }

    void SwitchToCurrentWeapon(){
        for (int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GameObject weapon = transform.Find(weapons[currentWeaponIndex].ToString()).gameObject;
        weapon.SetActive(true);
        weapon.GetComponent<WeaponBase>().UpdateTexts();
    }

    void Update()
    {
        CheckWeaponSwitch();
    }

    void CheckWeaponSwitch(){
        if(Input.GetButtonDown("SwitchWeaponsForward")){
            SelectNextWeapon();
        }
        if (Input.GetButtonDown("SwitchWeaponsBack"))
        {
            SelectPreviousWeapon();
        }
    }

    void SelectPreviousWeapon(){
        if(currentWeaponIndex== 0){
            currentWeaponIndex = weapons.Length - 1;
        }
        else{
            currentWeaponIndex--;
        }

        SwitchToCurrentWeapon();
    }

    void SelectNextWeapon(){
        if(currentWeaponIndex >= (weapons.Length - 1)){
            currentWeaponIndex = 0;
        }
        else{
            currentWeaponIndex++;
        }

        SwitchToCurrentWeapon();
    }
}
