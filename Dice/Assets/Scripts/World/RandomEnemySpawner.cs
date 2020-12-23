using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public int amountOfEnemies;
    public bool randomElementType = false;
    public bool randomEnemyType = false;

    public EnemyType enemy;
    public EElementalyType element;
    private GameObject enemyPrefab;
    private GameObject tempEnemy;
    private BoxCollider spawnArea;
    private Vector3 enemyPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnArea = transform.Find("Floor").GetComponent<BoxCollider>();
        for (int i = 0; i < amountOfEnemies; i++)
        {
            enemyPos = RandomPointInBounds(spawnArea.bounds);
            enemyPos.y = 2;

            if (randomEnemyType)
            {
                RandomEnemy();
            }

            if (randomElementType)
            {
                RandomElement();
            }


            enemyPrefab = Resources.Load("Enemies/" + enemy.ToString() + "/" + enemy.ToString() + element.ToString()) as GameObject;
            tempEnemy = MonoBehaviour.Instantiate(enemyPrefab, /*transform.position + */enemyPos, transform.rotation) as GameObject;
        }
    }


    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        
        return new Vector3(
            Random.Range(bounds.min.x + 1, bounds.max.x - 1),
            Random.Range(bounds.min.y + 1, bounds.max.y - 1),
            Random.Range(bounds.min.z + 1, bounds.max.z - 1)
        );
    }


    void RandomElement()
    {
        element = (EElementalyType)Random.Range(0, (int)EElementalyType.AmountOfElements - 1);
    }

    void RandomEnemy()
    {
        enemy = (EnemyType)Random.Range(0, (int)EnemyType.AmountOfEnemies);
    }


}

public enum EnemyType
{
    Golem = 0,
    Cactus,
    PlantMonster,
    Ghost,
    Bat,
    Knight,
    Skeleton,


    AmountOfEnemies,
}