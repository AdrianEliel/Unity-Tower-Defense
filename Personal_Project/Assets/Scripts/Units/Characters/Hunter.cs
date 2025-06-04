using UnityEngine;

public class Hunter : GeneralUnit
{
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
        if (checkForTarget(enemyLookedAt))
        {
            StartCoroutine(AttackFire(enemyLookedAt.transform.position));
        }
    }
}
