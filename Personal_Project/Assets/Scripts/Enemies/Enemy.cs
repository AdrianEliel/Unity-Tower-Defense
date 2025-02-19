using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header ("Navmesh Data")]
    public NavMeshAgent agent;
    public Transform destination;

    [Header("Enemy Data")]
    public EnemyData data;
    private int health;
    private float speed;
    private string enemyType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = data.speed;
        health = data.health;
        enemyType = checkEnemyType();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;
        agent.SetDestination(destination.position);
    }
    public string checkEnemyType()
    {
        if (data.vampire == true)
        {
            return "vampire";
        }
        else if (data.werewolf == true)
        {
            return "were";
        }
        else
        {
            return 3;
        }
    }
}
