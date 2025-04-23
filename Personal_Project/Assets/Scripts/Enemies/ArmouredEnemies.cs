using UnityEngine;

public class ArmouredEnemies : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        armor = GetComponentInChildren<GetArmor>().GetGameObject();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
