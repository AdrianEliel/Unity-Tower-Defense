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
    public int round;
    public int difficulty;
    public int gold;
    public GameObject[] Spawners;

    [Header("Round stats")]
    public int enemiesToSpawn;
    public int currentRound;
    public int weakRounds;
    public int moderateRounds;
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
        roundUI.updateRoundCounter(round);
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
    IEnumerator zombieRounds(int numRedEnemies)
    {
        for(int i =0; i < numRedEnemies; i++)
        {
            SpawnEnemy(enemies[0]);
            yield return new WaitForSeconds(.3f);
        }
    }

    IEnumerator vampireRounds(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if(i%3 == 0)
            {
                SpawnEnemy(enemies[1]);
            }
            else
            {
                SpawnEnemy(enemies[0]);
            }
            yield return new WaitForSeconds(.3f);
        }
    }
    IEnumerator werewolfRounds(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            if (i % 4 == 0)
            {
                SpawnEnemy(enemies[2]);
            }
            else if (i % 3 == 0)
            {
                SpawnEnemy(enemies[1]);
            }
            else
            {
                SpawnEnemy(enemies[0]);
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

            if (currentRound <= weakRounds)
            {
                RoundStartHelper();
                StartCoroutine(zombieRounds(enemiesToSpawn));
            }

            else if (currentRound <= moderateRounds)
            {
                RoundStartHelper();
                StartCoroutine(vampireRounds(enemiesToSpawn));
            }

            else if(currentRound%50 != 0)
            {
                RoundStartHelper();
                StartCoroutine(werewolfRounds(enemiesToSpawn));
            }
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
        enemiesToSpawn += currentRound;
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
