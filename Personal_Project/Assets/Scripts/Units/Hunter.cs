using UnityEngine;

public class Hunter : GeneralUnit
{
    public Transform weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        weapon = gameObject.GetComponentInChildren<GetGunData>().GetGunTransform();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (placed)
        {
            if (isInRange)
            {
                TryShoot();
            }
        }
    }

    public override void Shoot()
    {
        if (enemyLookedAt.name == (data.target.name + "(Clone)"))
        {
            StartCoroutine(AttackFire(enemyLookedAt.transform.position));
        }
    }
}
