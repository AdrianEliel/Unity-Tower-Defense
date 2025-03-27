using Unity.VisualScripting;
using UnityEngine;

public abstract class GeneralUnit : MonoBehaviour
{
    [Header("Script Refs")]
    public UnitData data;
    public RoundManager roundManager;

    [Header("Targeting variables")]
    public bool isInRange;

    [Header("Attacking")]
    public float nextTimeToFire;

    [Header("Enemy")]
    public GameObject enemyLookedAt;

    [Header("Buying & placing")]
    public bool canBePlaced = true;
    public float yOffset = 1;
    public bool placed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        placeUnit();
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

    public void placeUnit()
    {
        if (placed == false)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y));
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);

            if (Input.GetMouseButtonDown(0) && roundManager.gold>=data.price)
            {
                if(checkGround() && canBePlaced)
                {
                    placed = true;
                    roundManager.updateGoldAmount(-data.price);
                    
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }
            else if (Input.GetMouseButton(1))
            {
                Destroy(gameObject );
            }

            roundManager.isUnitBeingPlaced = false;
            roundManager.UnitBeingPlaced = null;
        }
    }

    private bool checkGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 2))
        {
            if (hit.transform.CompareTag("PlacementAllowed"))
            {
                return true;
            }
            
        }
        return false;
    }
    
    public void HandleShoot()
    {
        Shoot();
    }
    public abstract void Shoot();
}
