using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Script Refs")]
    private HealthLoss healthLoss;
    private RoundManager roundManager;

    [Header ("Movement Data")]
    public NavMeshAgent agent;
    public Transform destination;
    public int currentWaypoint;


    [Header("Enemy Data")]
    public EnemyData data;
    private int health;
    private float speed;
    private string enemyType;
    private int damageStrength;
    private int goldAmount;
    private bool atTheEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
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
        StartCoroutine(moveToWayPoints());
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

    private IEnumerator moveToWayPoints()
    {
        while(atTheEnd==false)
        {
            yield return null;
            if (roundManager.enemyWaypoints.Length > 1 && roundManager.enemyWaypoints[currentWaypoint] != null)
            {
                agent.SetDestination(roundManager.enemyWaypoints[currentWaypoint].position);
                if (Vector3.Distance(transform.position, roundManager.enemyWaypoints[currentWaypoint].position) < 1f)
                {
                    if (currentWaypoint < roundManager.enemyWaypoints.Length - 1)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        atTheEnd = true;
                    }
                }
            }
        }
        agent.SetDestination(destination.position);
        
        
        
    }
}
