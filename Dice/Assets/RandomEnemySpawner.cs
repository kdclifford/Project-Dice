using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public EnemyType enemy;
    public EElementalyType element;
    private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        enemy = (EnemyType)Random.Range(0, (int)EnemyType.AmountOfEnemies);
        element = (EElementalyType)Random.Range(0, (int)EElementalyType.AmountOfElements - 1);
        enemyPrefab = Resources.Load("Enemies/" + enemy.ToString() + "/" + enemy.ToString() + element.ToString()) as GameObject;

        GameObject tempEnemy = MonoBehaviour.Instantiate(enemyPrefab, transform.position, transform.rotation) as GameObject;
    }

 
}

public enum EnemyType
{
    Golem = 0,
    Cactus,
    PlantMonster,
    Ghost,

    AmountOfEnemies,
}