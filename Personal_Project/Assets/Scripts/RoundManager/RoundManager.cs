using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Game Stats")]
    public int health;
    public int round;
    public int difficulty;
    public GameObject[] Spawners;

    [Header("Round stats")]
    public int EnemiesToSpawn;
    public int currentRound;
    public int weakRounds;
    public int moderateRounds;
    public int strongRounds;

    [Header("Enemies")]
    public GameObject[] enemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemy(enemies[0]);
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
    public void spawnSpecialEnemy()
    {

    }
    public void startRound()
    {
        if(currentRound <= weakRounds)
        {

        }
    }
}
