using System.Collections;
using System.Transactions;
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
    public GameObject[] Spawners;

    [Header("Round stats")]
    public int enemiesToSpawn;
    public int currentRound;
    public int weakRounds;
    public int moderateRounds;
    public int strongRounds;
    public bool roundStarted;

    [Header("Enemies")]
    public GameObject[] enemies;
    public int enemiesKilled;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthLoss = GameObject.Find("EnemyDestination").GetComponent<HealthLoss>();
        roundUI = GetComponentInParent<RoundUI>();
        
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
    IEnumerator spawnWeakRound(int numEnemies)
    {
        for(int i =0; i < numEnemies; i++)
        {
            SpawnEnemy(enemies[0]);
            yield return new WaitForSeconds(1.4f);
        }
    }
    public void startRound()
    {
        if(roundStarted == false)
        {
            if (currentRound <= weakRounds)
            {
                currentRound++;
                roundUI.updateRoundCounter(currentRound);
                roundStarted = true;
                StartCoroutine(spawnWeakRound(enemiesToSpawn));
            }
        }
        
    }
    public void updateEnemiesToSpawn()
    {
        enemiesToSpawn += currentRound;
    }

    public void loseHealth(int num)
    {
        health -= num;
    }
    
}
