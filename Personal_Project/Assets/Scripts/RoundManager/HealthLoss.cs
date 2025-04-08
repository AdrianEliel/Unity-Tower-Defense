using UnityEngine;

public class HealthLoss : MonoBehaviour
{
    private RoundManager roundManager;

    public int enemiesDestroyed;

    [Header("Unit Data")]
    public UnitData unitData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Destroy(other.gameObject);
            enemiesDestroyed++;
            roundManager.loseHealth(enemy.getStrength());
            if (enemiesDestroyed + roundManager.enemiesKilled == roundManager.enemiesToSpawn)
            { 
                roundManager.updateEnemiesToSpawn();
                roundManager.enemiesKilled = 0;
                enemiesDestroyed = 0;
                roundManager.roundStarted = false;
            }
        }
    }

}
