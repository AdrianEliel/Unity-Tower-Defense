using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public abstract class GeneralUnit : MonoBehaviour
{
    [Header("Script Refs")]
    public UnitData data;
    public RoundManager roundManager;

    [Header("Targeting variables")]
    public Rigidbody rb;
    public bool isInRange;

    [Header("Attacking")]
    public float nextTimeToFire;
    public Transform weapon;

    [Header("Enemy")]
    public GameObject enemyLookedAt;

    [Header("Buying & placing")]
    public bool canBePlaced = true;
    public bool placed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        roundManager = GameObject.Find("Round Manager").GetComponent<RoundManager>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    public virtual void Update()
    {
        placeUnit();

    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && checkForTarget(other.gameObject))
        {
            isInRange = true;
            Vector3 enemyPos = new Vector3(other.transform.position.x, 1, other.transform.position.z);
            transform.LookAt(enemyPos);
            enemyLookedAt = other.gameObject;
        }

        if (other.gameObject.CompareTag("PlacementRangeCollider"))
        {
            canBePlaced = false;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isInRange = false;
            enemyLookedAt = null;
        }

        if (other.gameObject.CompareTag("PlacementRangeCollider"))
        {
            canBePlaced = true;
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
            if (hit.transform.CompareTag("UnitPlacementAllowed"))
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

    public bool checkForTarget(GameObject enemyLookedAt)
    {
        foreach (GameObject enemy in data.targets)
        {
            if (enemy.name+"(Clone)" == enemyLookedAt.name)
            {
                return true;
            }
        }
        return false;
    }

    protected IEnumerator AttackFire(Vector3 target)
    {
        Enemy enemy = null;
        GameObject attackTrail = Instantiate(data.attackTrail, weapon.transform.position, Quaternion.identity);
        if (attackTrail != null)
        {
            BulletController bulletController = attackTrail.GetComponent<BulletController>();
        }

        if (enemyLookedAt != null)
        {
            enemy = enemyLookedAt.GetComponent<Enemy>();
        }


        while (attackTrail != null && Vector3.Distance(attackTrail.transform.position, target) > .001f)
        {
            attackTrail.transform.position = Vector3.MoveTowards(attackTrail.transform.position, target, Time.deltaTime * data.attackSpeed);
            attackTrail.transform.LookAt(target);
            yield return null;
        }

        Destroy(attackTrail);


        Debug.Log(enemyLookedAt + " was hit");
        enemy.takeDamage();

    }
}
