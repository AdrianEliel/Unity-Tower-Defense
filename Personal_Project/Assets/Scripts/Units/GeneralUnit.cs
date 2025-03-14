using Unity.VisualScripting;
using UnityEngine;

public abstract class GeneralUnit : MonoBehaviour
{
    [Header("Script Refs")]
    public UnitData data;

    [Header("Targeting variables")]
    public bool isInRange;

    [Header("Attacking")]
    public float nextTimeToFire;

    [Header("Enemy")]
    public GameObject enemyLookedAt;

    [Header("Buying & placing")]
    public float prespectiveOffset = 10.5f;
    public bool placed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (placed == false)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                placed = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isInRange = true;
            transform.LookAt(other.transform.position);
            enemyLookedAt = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isInRange = false;
            enemyLookedAt = null;
        }
    }

    public void TryShoot()
    {
        if (enemyLookedAt!=null)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + (1 / data.attackRate);
                HandleShoot();
            }
        }
        

    }
    
    public void HandleShoot()
    {
        Shoot();
    }
    public abstract void Shoot();
}
