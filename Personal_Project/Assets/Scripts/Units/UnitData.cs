using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitData")]
public class UnitData : ScriptableObject
{
    [Header("Attacks")]
    public GameObject attack;

    [Header("Targets")]
    public GameObject target;
}
