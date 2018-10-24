using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    Ready,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour {
    public Viewbase startView;
    public Transform playerSpawnPoint;
    public GameObject playerPrefab;
    public static GameManager instance;
    [HideInInspector] public GameState gameState = GameState.Ready;
    public Transform[] enemySpawnPoints;
    public EnemySpawner enemySpawner;
    public float spawnDuration = 5f;
    public int maxZombies = 20;
    public int zombiesSpawned = 0;
    public int baseZombieHP = 100;
    public float baseZombieSpeed = 1.0f;
    [SerializeField] private int zombieHP;
    [SerializeField] private float zombieSpeed;
    [SerializeField] private int killReward;
    public float upgradeDuration = 20f;
    public float maxZombieSpeed = 5.0f;
    public int baseKillReward = 10;
    public Transform maxAmmoObj;
    public Transform healthObj;
    public Transform speedBoostObj;
    public Transform doubleScoreObj;
    public Transform monsterObj;
    public static int maxPowerups = 0;
    public static int maxHealthCount = 0;
    public static int speedBoostCount = 0;
    public static int doubleScoreCount = 0;
    public static int healthCount = 0;

    private IEnumerator coSpawnEnemies;
    private IEnumerator coEnhanceZombieStatus;

    void Awake()
    {
        instance = this;
    }

    public void startGame(){
        zombieHP = baseZombieHP;
        zombieSpeed = baseZombieSpeed;
        killReward = baseKillReward;
        Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
       //coSpawnEnemies = CoSpawnEnemies();
       // coEnhanceZombieStatus = CoEnhanceZombieStatus();
        StartCoroutine(CoSpawnEnemies());
        StartCoroutine(CoEnhanceZombieStatus());
        gameState = GameState.Playing;
    }

    void Update()
    {
        if(maxPowerups < 4){
            int randNumber = Random.Range(0, 19);
            if(maxHealthCount < 1){
                if (randNumber == 4)
                {
                    int randNumber1 = Random.Range(0, 6);
                    if(randNumber1 == 1){
                        Instantiate(maxAmmoObj, new Vector3(1, 0, 13), maxAmmoObj.rotation);
                        maxHealthCount++;
                        maxPowerups++;
                    }
                    if (randNumber1 == 2)
                    {
                        Instantiate(maxAmmoObj, new Vector3(-21, 0, -4), maxAmmoObj.rotation);
                        maxHealthCount++;
                        maxPowerups++;
                    }
                    if (randNumber1 == 3)
                    {
                        Instantiate(maxAmmoObj, new Vector3(-3, 0, -21), maxAmmoObj.rotation);
                        maxHealthCount++;
                        maxPowerups++;
                    }
                    if (randNumber1 == 4)
                    {
                        Instantiate(maxAmmoObj, new Vector3(27, 0, -4), maxAmmoObj.rotation);
                        maxHealthCount++;
                        maxPowerups++;
                    }
                    if (randNumber1 == 5)
                    {
                        Instantiate(maxAmmoObj, new Vector3(19, 0, 27), maxAmmoObj.rotation);
                        maxHealthCount++;
                        maxPowerups++;
                    }

                }
            }
           
            if(healthCount < 1){
                if (randNumber == 9)
                {
                    int randNum2 = Random.Range(0, 6);
                    if(randNum2 == 1){
                        Instantiate(healthObj, new Vector3(1, 0, 15), healthObj.rotation);
                        healthCount++;
                        maxPowerups++;
                    }
                    if (randNum2 == 2)
                    {
                        Instantiate(healthObj, new Vector3(-10, 0, 28), healthObj.rotation);
                        healthCount++;
                        maxPowerups++;
                    }
                    if (randNum2 == 3)
                    {
                        Instantiate(healthObj, new Vector3(2, 0, -13), healthObj.rotation);
                        healthCount++;
                        maxPowerups++;
                    }
                    if (randNum2 == 4)
                    {
                        Instantiate(healthObj, new Vector3(-25, 0, -23), healthObj.rotation);
                        healthCount++;
                        maxPowerups++;
                    }
                    if (randNum2 == 5)
                    {
                        Instantiate(healthObj, new Vector3(-25, 0, 28), healthObj.rotation);
                        healthCount++;
                        maxPowerups++;
                    }

                }
            }

            if(speedBoostCount < 1){
                if (randNumber == 14)
                {
                    int randNum4 = Random.Range(0, 6);
                    if(randNum4 == 1){
                        Instantiate(speedBoostObj, new Vector3(1, 0, 17), speedBoostObj.rotation);
                        speedBoostCount++;
                        maxPowerups++;
                    }
                    if (randNum4 == 2)
                    {
                        Instantiate(speedBoostObj, new Vector3(1, 0, 17), speedBoostObj.rotation);
                        speedBoostCount++;
                        maxPowerups++;
                    }
                    if (randNum4 == 3)
                    {
                        Instantiate(speedBoostObj, new Vector3(1, 0, 17), speedBoostObj.rotation);
                        speedBoostCount++;
                        maxPowerups++;
                    }
                    if (randNum4 == 4)
                    {
                        Instantiate(speedBoostObj, new Vector3(1, 0, 17), speedBoostObj.rotation);
                        speedBoostCount++;
                        maxPowerups++;
                    }
                    if (randNum4 == 5)
                    {
                        Instantiate(speedBoostObj, new Vector3(1, 0, 17), speedBoostObj.rotation);
                        speedBoostCount++;
                        maxPowerups++;
                    }
                }
            }

            if(doubleScoreCount < 1){
                if (randNumber == 18)
                {
                    int randNum3 = Random.Range(0, 6);
                    if(randNum3 == 1){
                        Instantiate(doubleScoreObj, new Vector3(1, 0, 19), doubleScoreObj.rotation);
                        doubleScoreCount++;
                        maxPowerups++;
                    }
                    if (randNum3 == 2)
                    {
                        Instantiate(doubleScoreObj, new Vector3(-18, 0, 15), doubleScoreObj.rotation);
                        doubleScoreCount++;
                        maxPowerups++;
                    }
                    if (randNum3 == 3)
                    {
                        Instantiate(doubleScoreObj, new Vector3(27, 0, 19), doubleScoreObj.rotation);
                        doubleScoreCount++;
                        maxPowerups++;
                    }
                    if (randNum3 == 4)
                    {
                        Instantiate(doubleScoreObj, new Vector3(3, 0, -29), doubleScoreObj.rotation);
                        doubleScoreCount++;
                        maxPowerups++;
                    }
                    if (randNum3 == 5)
                    {
                        Instantiate(doubleScoreObj, new Vector3(-16, 0, -14), doubleScoreObj.rotation);
                        doubleScoreCount++;
                        maxPowerups++;
                    }
                }
            }
           
        }

    }

    public void GameOver(){
        StopCoroutine(coSpawnEnemies);
        StopCoroutine(coEnhanceZombieStatus);
        gameState = GameState.GameOver;
    }

    public void ResetGame(){
        Enemy[] zombies = GameObject.FindObjectsOfType<Enemy>();
        for (int i = 0; i < zombies.Length; i++){
            Destroy(zombies[i].gameObject);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameState = GameState.Ready;
        startView.Show();
    }

    IEnumerator CoSpawnEnemies(){
        yield return new WaitForSeconds(5);
        while(true){
            Instantiate(monsterObj, new Vector3(1, 0, 11), monsterObj.rotation);
            for (int i = 0; i < enemySpawnPoints.Length; i++)
            {
                if (zombiesSpawned >= maxZombies) continue;
                GameObject enemyGameObj = enemySpawner.SpawnAt(enemySpawnPoints[i].position, enemySpawnPoints[i].rotation);
                Enemy enemy = enemyGameObj.GetComponent<Enemy>();
                Health enemyHealth = enemyGameObj.GetComponent<Health>();
                KillReward enemykillReward = enemyGameObj.GetComponent<KillReward>();

                enemy.speed = zombieSpeed;
                enemyHealth.value = zombieHP;
                enemykillReward.amount = killReward;

                enemy.onDead.AddListener(() =>
                {
                    zombiesSpawned--;
                });

                zombiesSpawned++;
            }

            yield return new WaitForSeconds(spawnDuration);
        }
    }

    IEnumerator CoEnhanceZombieStatus(){
        yield return new WaitForSeconds(5);
        while (true){
            yield return new WaitForSeconds(upgradeDuration);


            zombieHP += 20;
            zombieSpeed += 0.25f;
            killReward++;

            if(zombieSpeed > maxZombieSpeed){
                zombieSpeed = maxZombieSpeed;
            }
        }
    }
}
