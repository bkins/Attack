using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject Prefab;
    public float      SpawnInterval = 5f;
    public float      MoveSpeed     = 2f;
}
