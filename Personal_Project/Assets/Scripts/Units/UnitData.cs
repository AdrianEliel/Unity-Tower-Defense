using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Attacks")]
    public string weaponName;
    public GameObject attackTrail;
    public float attackSpeed;

    [Header("Targets")]
    public GameObject target;
    public LayerMask targetLayer;
}
