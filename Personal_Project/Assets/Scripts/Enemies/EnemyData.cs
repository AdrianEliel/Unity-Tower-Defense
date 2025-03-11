using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header ("Enemy Stats")]
    public int health;
    public float speed;
    public int damageStrength;
    public int goldAmount;

    [Header("Enemy Type")]
    public bool vampire;
    public bool zombie;
    public bool werewolf;
}
