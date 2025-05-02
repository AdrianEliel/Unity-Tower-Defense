using System;
using System.Collections;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Script Refs")]
    private HealthLoss healthLoss;
    private RoundUI roundUI;

    [Header("Game Stats")]
    public int health;
    public int gold;
    public GameObject[] Spawners;

    [Header("Round stats")]
    public int enemiesToSpawn;
    public int bossesToSpawn;
    public int currentRound;
    public int weakRounds;
    public int moderateRounds;
    public int harderRounds;
    public bool roundStarted;

    [Header("Enemies")]
    public GameObject[] bosses;
    public GameObject[] enemies;
    public int enemiesKilled;

    [Header("Enemy data")]
    public EnemyData zombie;
    public EnemyData vampire;
    public EnemyData wereWolf;

    [Header("Enemy Movement")]
    public Transform[] enemyWaypoints;
    

    [Header("Unit placement")]
    public bool isUnitBeingPlaced;
    public GameObject UnitBeingPlaced;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // sets up the variables and ui for the scene
    void Start()
    { 
        healthLoss = GameObject.Find("Enemy Destination").GetComponent<HealthLoss>();
        roundUI = GetComponentInParent<RoundUI>();
        roundUI.updateHealthCounter(health);
        roundUI.updateRoundCounter(currentRound);
        roundUI.updateMoneyCounter(gold);
    }

    // Update is called once per frame
    void Update()
    {
         RoundEndHelper();
    }
    //takes in the enemy gameobject and instantiates it at a specific location - enemy is brought from the list
    public void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPos = Spawners[UnityEngine.Random.Range(0,Spawners.Length)].transform.position;
        Instantiate(enemy, spawnPos, transform.rotation);
    }

    //The Method to Spawn enemies. Takes in the number of each type of enemy that will spawn, the does a reverse forloop that subtratcs its index for every enemy that spawns
    //Based on however that can spawn, the program will spawn the enemies in the order you give it.
    IEnumerator SpawnEnemies(int numZombies, int numArmoredZombies, int numVampires, int numWerewolves, int numBats)
    {
        for(int i=enemiesToSpawn; i > 0; i--)
        {
            if (i > (Mathf.Abs(enemiesToSpawn - numZombies)))
            {
                SpawnEnemy(enemies[0]);
            }
            else if(i> (Mathf.Abs(enemiesToSpawn-numZombies-numArmoredZombies)))
            {
                SpawnEnemy(enemies[3]);
            }
            else if(i> (Mathf.Abs(enemiesToSpawn - numZombies - numArmoredZombies - numVampires)))
            {
                SpawnEnemy(enemies[1]);
            }
            else if(i> (Mathf.Abs(enemiesToSpawn - numZombies - numArmoredZombies - numVampires - numWerewolves)))
            {
                SpawnEnemy(enemies[2]);
            }
            else if(i> (Mathf.Abs(enemiesToSpawn - numZombies - numArmoredZombies - numVampires - numWerewolves - numBats)))
            {
                SpawnEnemy(enemies[4]);
            }
            yield return new WaitForSeconds(.3f);
        }
    }
    IEnumerator SpawnBossRound(int numCoffins){
        for(int i=bossesToSpawn; i>0;i--)
        {
            if(i>(Math.Abs(bossesToSpawn-numCoffins)))
            {
                SpawnEnemy(bosses[0]);
            }
            yield return new WaitForSeconds(.3f);
        }
    }
    //Start button calls this method. Starts the round with the start helper method and depending on what round its on, will use the spawn enemies
    //method differently
    public void startRound()
    {
        if(roundStarted == false)
        {
            RoundStartHelper();
            
            if (currentRound == 1)
            {
                StartCoroutine(SpawnEnemies(20,0,0,0,0));
            }
            else if(currentRound == 2)
            {
                StartCoroutine(SpawnEnemies(15, 5, 0, 0, 0));
            }
            else if(currentRound == 3)
            {
                StartCoroutine(SpawnEnemies(20, 5,0, 0, 0));
            }
            else if (currentRound == 4)
            {
                StartCoroutine(SpawnEnemies(15, 10, 0, 0, 0));
            }
            else if (currentRound == 5)
            {
                StartCoroutine(SpawnEnemies(20, 10, 0, 0, 0));
            }
            else if (currentRound == 6)
            {
                StartCoroutine(SpawnEnemies(10, 15, 5, 0, 0));
            }
            else if (currentRound == 7)
            {
                StartCoroutine(SpawnEnemies(20, 15, 0, 0, 0));
            }
            else if (currentRound == 8)
            {
                StartCoroutine(SpawnEnemies(10, 20, 5, 0, 0));
            }
            else if (currentRound == 9)
            {
                StartCoroutine(SpawnEnemies(15, 15, 10, 0, 0));
            }
            else if(currentRound == 10)
            {
                StartCoroutine(SpawnEnemies(5, 15, 15, 5, 0));
            }
            else if(currentRound == 11)
            {
                StartCoroutine(SpawnEnemies(5, 15,15, 10, 0));
            }
            else if (currentRound == 12)
            {
                StartCoroutine(SpawnEnemies(0, 20, 15, 10, 0));
            }
            else if (currentRound == 13)
            {
                StartCoroutine(SpawnEnemies(0, 20, 20, 10, 0));
            }
            else if (currentRound == 14)
            {
                StartCoroutine(SpawnEnemies(0, 20, 15, 15, 5));
            }
            else if (currentRound == 15)
            {
                StartCoroutine(SpawnEnemies(0, 15, 15, 15, 10));
                StartCoroutine(SpawnBossRound(1));
            }

        }
    }
    //The method that updates the variables needed for a round to begin
    private void RoundStartHelper()
    {
        if (currentRound > 0)
        {
            updateGoldAmount(100+currentRound);
        }
        currentRound++;
        roundUI.updateRoundCounter(currentRound);
        roundStarted = true;
    }
    //method that updates the variables needed to end the round - all enemies have to be killed for this to work
    private void RoundEndHelper()
    {
        if (healthLoss.enemiesDestroyed + enemiesKilled == enemiesToSpawn)
        {
            enemiesKilled = 0;
            healthLoss.enemiesDestroyed = 0;
            updateEnemiesToSpawn();
            roundStarted = false;
        }
    }
    //every 2 rounds the amount of enemies that will spawn increased by 5
    public void updateEnemiesToSpawn()
    {
        if (currentRound % 2 == 0)
        {
            enemiesToSpawn += 5;
        }
        if (currentRound%15==0){
            bossesToSpawn++;
        }
    }
    //enemies call this helper method when they reach their final destination
    public void loseHealth(int num)
    {
        health -= num;
        roundUI.updateHealthCounter(health);
    }
    //an enemy calls this method when it is killed and it is also called when the round ends
    public void updateGoldAmount(int goldAddSub)
    {
        gold += goldAddSub;
        roundUI.updateMoneyCounter(gold);
    }

}
