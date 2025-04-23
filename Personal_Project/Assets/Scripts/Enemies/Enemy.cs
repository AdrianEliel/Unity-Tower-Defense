using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    [Header("Script Refs")]
    private HealthLoss healthLoss;
    private RoundManager roundManager;

    [Header("Movement Data")]
    public NavMeshAgent agent;
    public Transform destination;
    public int currentWaypoint;


    [Header("Enemy Data")]
    public Rigidbody rb;
    public EnemyData data;
    public GameObject armor;
    [SerializeField] private int health;
    private float speed;
    private int damageStrength;
    private int goldAmount;
    private bool atTheEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
        rb = GetComponent<Rigidbody>(); 
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
        healthLoss = GameObject.Find("Enemy Destination").GetComponent<HealthLoss>();
        destination = GameObject.Find("Enemy Destination").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        health = data.health; 
        damageStrength = data.damageStrength;
        goldAmount = data.goldAmount;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        LiveOrDie();
        StartCoroutine(moveToWayPoints());

    }
    private void FixedUpdate()
    {
        
    }
    public int getStrength()
    {
        return damageStrength;
    }

    public void takeDamage()
    {
        health--;
        if ((armor!=null))
        {
            Destroy(armor);
        }
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
        while (atTheEnd == false)
        {
            yield return null;
            if (roundManager.enemyWaypoints.Length > 1 && roundManager.enemyWaypoints[currentWaypoint] != null)
            {
                if(isActiveAndEnabled)
                {
                    agent.SetDestination(roundManager.enemyWaypoints[currentWaypoint].position);
                }

                if (Vector3.Distance(transform.position, roundManager.enemyWaypoints[currentWaypoint].position) < .5f)
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
