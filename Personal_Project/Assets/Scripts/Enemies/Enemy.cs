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
    private int damageStrength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = GameObject.Find("EnemyDestination").transform;
        enemyType = checkEnemyType();
        speed = data.speed;
        health = data.health; 
        damageStrength = data.damageStrength;
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;
        agent.SetDestination(destination.position);
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
            Destroy(gameObject);
        }
    }
}
