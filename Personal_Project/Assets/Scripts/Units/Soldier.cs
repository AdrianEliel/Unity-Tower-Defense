using System.Collections;
using UnityEngine;

public class Soldier : GeneralUnit
{
    private Transform gun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        gun = CustomFindChild(data.weaponName, transform);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isInRange)
        {
            StartCoroutine(CanShoot());
        }
    }

    IEnumerator CanShoot()
    {
        Shoot();
        yield return new WaitForSeconds(.5f);
    }

    public override void Shoot()
    {
        RaycastHit hit;
        Vector3 target = Vector3.zero;
        if (Physics.Raycast(gun.position, transform.forward, out hit, 1000, data.targetLayer))
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
