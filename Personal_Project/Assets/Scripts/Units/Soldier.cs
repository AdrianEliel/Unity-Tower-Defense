using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : GeneralUnit
{
    public Transform gun;
    public Transform targetWorld;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        gun = gameObject.GetComponentInChildren<GetGunData>().GetGunTransform();
       
    }   

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isInRange)
        {
            TryShoot();
        }
    }

    public override void Shoot()
    {
        StartCoroutine(AttackFire(enemyLookedAt.transform.position));
    }
    
    IEnumerator AttackFire(Vector3 target)
    {
        Enemy enemy = null;
        GameObject attackTrail = Instantiate(data.attackTrail, gun.transform.position, Quaternion.identity);
        if(attackTrail != null)
        {
            BulletController bulletController = attackTrail.GetComponent<BulletController>();
        }

        if (enemyLookedAt != null)
        {
           enemy = enemyLookedAt.GetComponent<Enemy>();
        }
        

        while (attackTrail!=null && Vector3.Distance(attackTrail.transform.position, target)>.001f)
        {
            attackTrail.transform.position = Vector3.MoveTowards(attackTrail.transform.position, target, Time.deltaTime * data.attackSpeed);
            yield return null;
        }

        Destroy(attackTrail);

        
        Debug.Log(enemyLookedAt + " was hit");
        enemy.takeDamage();
        
    }


}
