using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool enemyTouched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTouched = true;
            Destroy(gameObject);
        }
    }
}
