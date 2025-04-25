using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Attacks")]
    public string weaponName;
    public GameObject attackTrail;
    public float attackRate;
    public float attackSpeed;

    [Header("Targets")]
    public GameObject[] targets;
    public LayerMask targetLayer;

    [Header("Unit Stats")]
    public int health;
    public int price;
    public int enemiesEliminated;
    public LayerMask placeLayer;

}
