using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorData
{
    public string name;
    public EnemyType[] enemiesAllowedToSpawn;
    public EElementalyType[] floorTypes;
}
