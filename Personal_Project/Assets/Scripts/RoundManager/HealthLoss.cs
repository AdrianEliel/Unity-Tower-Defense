using UnityEngine;

public class HealthLoss : MonoBehaviour
{
    private RoundManager roundManager;

    public int enemiesDestroyed;
    
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
            Destroy(other.gameObject);
            enemiesDestroyed++;
        }
    }
}
