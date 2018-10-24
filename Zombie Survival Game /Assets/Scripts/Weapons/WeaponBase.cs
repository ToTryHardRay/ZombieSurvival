using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum FireMode{
    SemiAuto,

    FullAuto
}

public class WeaponBase : MonoBehaviour {
    protected AudioSource audioSource;
    protected Animator animator;
    protected bool fireLock = false;
    protected bool canShoot = false;
    protected bool isReloading = false;
    protected CashSystem cashSystem;

    [Header("Object References")]
    public ParticleSystem muzzleflash;
    public Transform shootPoint;


    [Header("Sound References")]
    public AudioClip fireSound;
    public AudioClip dryFireSound;
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip boltSound;
    public AudioClip drawSound;

    [Header("UI References")]
    public Text weaponNameText;
    public Text ammoText;

    [Header("Weapon Attributes")]
    public int pellets = 1;
    public FireMode fireMode = FireMode.FullAuto;
    public float damage = 20f;
    public float fireRate = 1.0f;
    public int bulletsInClip;
    public int bulletsLeft;
    public int clipSize = 12;
    public int maxAmmo = 100;

    private void Start()
    {
        Transform inGameUITransform = GameObject.Find("/Canvas/InGame").transform;
        weaponNameText = inGameUITransform.Find("WeaponNameText").GetComponent<Text>();
        ammoText = inGameUITransform.Find("AmmoText").GetComponent<Text>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        cashSystem = player.GetComponent<CashSystem>();
        bulletsInClip = clipSize;
        bulletsLeft = maxAmmo;

        //wait until weapon can fire
        Invoke("EnableWeapon", 1f);
        Invoke("UpdateTexts", Time.deltaTime);
    }

    private void Update()
    {
        if(fireMode == FireMode.FullAuto && Input.GetButton("Fire1")){
            checkFire();
        }
        else if(fireMode == FireMode.SemiAuto && Input.GetButtonDown("Fire1")){
            checkFire();
        }
        if(Input.GetButtonDown("Reload")){
            CheckReload();
        }

    }

    public void UpdateTexts(){
        weaponNameText.text = GetWeaponName();
        ammoText.text = "Ammo:" + bulletsInClip + " / " + bulletsLeft;
    }

    string GetWeaponName(){
        string weaponName = "";

        if (this is Police9mm){
            weaponName = "Police 9mm";
        }
        else if(this is PortableMagnum){
            weaponName = "Portable Magnum";
        }
        else if(this is Compact9mm){
            weaponName = "Compact 9mm";
        }
        else if(this is UMP45){
            weaponName = "UMP45";
        }
        else if(this is StovRifle){
            weaponName = "Stov Rifle";
        }
        else {
            throw new System.Exception("unknown weapon");
        }
        return weaponName;
    }

    void EnableWeapon(){
        canShoot = true;
    }
    void checkFire(){
        if (!canShoot) return;
        if (fireLock) return;
        if(bulletsInClip > 0){
            Fire();
        }
        else{
            DryFire();
        }
    }

    void Fire(){
        audioSource.PlayOneShot(fireSound);
        fireLock = true;

        for (int i = 0; i < pellets; i ++){
            DirectHit();
        }



        muzzleflash.Stop();
        muzzleflash.Play();

        PlayFireAnimation();

        bulletsInClip--;
        UpdateTexts();

        StartCoroutine(CoResetFireLock());
    }

    public virtual void PlayFireAnimation(){
        animator.CrossFadeInFixedTime("Fire", 0.1f);
    }

    void DryFire(){
        audioSource.PlayOneShot(dryFireSound);
        fireLock = true;

        StartCoroutine(CoResetFireLock());
    }

    void DirectHit()
    {
        RaycastHit hit;

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit)){
            if(hit.transform.CompareTag("Enemy")){
                Health targetHealth = hit.transform.GetComponent<Health>();

                if(targetHealth == null){
                    throw new System.Exception("Cannot find health component on enemy");
                }

                else{
                    targetHealth.TakeDamage(damage);

                    if(targetHealth.value <= 0){
                        KillReward killReward = hit.transform.GetComponent<KillReward>();

                        if(killReward == null){
                            throw new System.Exception("cannot find kill reward on enemy");
                        }

                        cashSystem.cash += killReward.amount;
                    }
                }
            }
              
        }
    }

    IEnumerator CoResetFireLock() {
        yield return new WaitForSeconds(fireRate);
        fireLock = false;
    }

    void CheckReload(){
        if(bulletsLeft > 0 && bulletsInClip < clipSize){
            Reload();
        }
    }

    protected virtual void Reload(){
        int bulletsToLoad = clipSize - bulletsInClip;
        int bulletsToSub = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToSub;
        bulletsInClip += bulletsToLoad;
        UpdateTexts();
        animator.CrossFadeInFixedTime("Reload", 0.1f);
    }
}
