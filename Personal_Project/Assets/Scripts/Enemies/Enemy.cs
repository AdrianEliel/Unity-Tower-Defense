using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Script Refs")]
    private HealthLoss healthLoss;
    private RoundManager roundManager;

    [Header ("Navmesh Data")]
    public NavMeshAgent agent;
    public Transform destination;


    [Header("Enemy Data")]
    public EnemyData data;
    private int health;
    private float speed;
    private string enemyType;
    private int damageStrength;
    private int goldAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
        healthLoss = GameObject.Find("EnemyDestination").GetComponent<HealthLoss>();
        destination = GameObject.Find("EnemyDestination").transform;
        enemyType = checkEnemyType();
        speed = data.speed;
        health = data.health; 
        damageStrength = data.damageStrength;
        goldAmount = data.goldAmount;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;
        moveToWayPoints();
        LiveOrDie();
    }
    public string checkEnemyType()
    {
        if (data.vampire == true)
        {
            return "vampire";
        }
        else if (data.werewolf == true)
        {
            return "werewolf";
        }
        else
        {
            return "zombie";
        }
    }

    public int getStrength()
    {
        return damageStrength;
    }

    public void takeDamage()
    {
        health--;
    }

    private void LiveOrDie()
    {
        if (health <= 0)
        {
            roundManager.enemiesKilled++;
            roundManager.updateGoldAmount(goldAmount);
            if (healthLoss.enemiesDestroyed + roundManager.enemiesKilled == roundManager.enemiesToSpawn)
            {
                roundManager.updateEnemiesToSpawn();
                roundManager.enemiesKilled = 0;
                healthLoss.enemiesDestroyed = 0;
                roundManager.roundStarted = false;
            }
            Destroy(gameObject);
        }
    }

    private void moveToWayPoints()
    {
        agent.SetDestination(destination.position);
    }
}
