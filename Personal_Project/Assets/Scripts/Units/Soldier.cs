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
        RaycastHit hit;
        Vector3 target = Vector3.zero;
        if (Physics.Raycast(gun.position, enemyLookedAt.transform.position, out hit, 1000, data.targetLayer))
        {
            Debug.Log(hit.collider.name + " was hit");
            target = hit.point;
            StartCoroutine(AttackFire(target));
        }
    }
    
    IEnumerator AttackFire(Vector3 target)
    {
        GameObject attackTrail = Instantiate(data.attackTrail, gun.transform);

        while (attackTrail!=null&& Vector3.Distance(gun.transform.position, target) >.1f)
        {
            attackTrail.transform.position = Vector3.MoveTowards(attackTrail.transform.position, target, Time.deltaTime * data.attackSpeed);
        }

        yield return null;

        Destroy(attackTrail);
    }


}
