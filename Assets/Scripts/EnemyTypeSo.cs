using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyType", menuName = "Game/Enemy Type")]
public class EnemyTypeSo : ScriptableObject
{
    public GameObject Prefab;
    public float      SpawnInterval = 5f;
    public float      MoveSpeed     = 2f;
    public float      SpawnWeight   = 1f; // Higher = more likely
}