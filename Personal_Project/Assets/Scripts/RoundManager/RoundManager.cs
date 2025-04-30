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
    public int currentRound;
    public int weakRounds;
    public int moderateRounds;
    public int harderRounds;
    public bool roundStarted;

    [Header("Enemies")]
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

    }
    public void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPos = Spawners[Random.Range(0,Spawners.Length)].transform.position;
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

    public void startRound()
    {
        if(roundStarted == false)
        {
            if (currentRound != 0)
            {
                updateGoldAmount(100+currentRound);
            }
            if (currentRound == 0)
            {
                StartCoroutine(SpawnEnemies(20,0,0,0,0));
            }
            else if(currentRound == 1)
            {
                StartCoroutine(SpawnEnemies(15, 5, 0, 0, 0));
            }
            else if(currentRound == 2)
            {
                StartCoroutine(SpawnEnemies(20, 5,0, 0, 0));
            }
            else if (currentRound == 3)
            {
                StartCoroutine(SpawnEnemies(15, 10, 0, 0, 0));
            }
            else if (currentRound == 4)
            {
                StartCoroutine(SpawnEnemies(20, 10, 0, 0, 0));
            }
            else if (currentRound == 5)
            {
                StartCoroutine(SpawnEnemies(10, 15, 5, 0, 0));
            }
            else if (currentRound == 6)
            {
                StartCoroutine(SpawnEnemies(20, 5, 0, 0, 0));
            }
            else if (currentRound == 7)
            {
                StartCoroutine(SpawnEnemies(10, 20, 5, 0, 0));
            }
            else if (currentRound == 8)
            {
                StartCoroutine(SpawnEnemies(15, 15, 10, 0, 0));
            }

            RoundStartHelper();
        }
    }
    private void RoundStartHelper()
    {
        currentRound++;
        roundUI.updateRoundCounter(currentRound);
        roundStarted = true;
    }
    public void updateEnemiesToSpawn()
    {
        if (currentRound % 2 == 0)
        {
            enemiesToSpawn += 5;
        }
    }

    public void loseHealth(int num)
    {
        health -= num;
        roundUI.updateHealthCounter(health);
    }

    public void updateGoldAmount(int goldAddSub)
    {
        gold += goldAddSub;
        roundUI.updateMoneyCounter(gold);
    }

}
