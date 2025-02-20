using UnityEngine;

public class EnemyInRange : MonoBehaviour
{
    public Collider rangeCollider;
    public Transform unitTransform;
    public bool enemyInRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = true;
            unitTransform.LookAt(other.transform.position);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
        }
    }


}
