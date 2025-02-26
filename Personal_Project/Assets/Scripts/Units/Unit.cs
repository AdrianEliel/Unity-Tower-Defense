using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Script Refs")]
    private EnemyInRange enemyInRange;
    [Header ("Player Stats")]
    public int enemiesDefeated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyInRange = GetComponentInParent<EnemyInRange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyInRange.isInRange == true)
        {

        }
    }
}
