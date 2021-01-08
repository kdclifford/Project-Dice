using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public int currentFloor = 0;
    public FloorData[] floors;
    public List<GameObject> list;
    public List<Room> roomList;
   // [HideInInspector]
    public GameObject[] enemyList;



    private void Awake()
    {
        int enemyTotal = (int)EnemyType.AmountOfEnemies * ((int)EElementalyType.AmountOfElements - 1);
        enemyList = new GameObject[enemyTotal];



        for (int i = 0; i < (int)EnemyType.AmountOfEnemies; i++)
        {
            EnemyType enemy = (EnemyType)i;
            for (int j = 0; j < (int)EElementalyType.AmountOfElements - 1; j++)
            {
                EElementalyType element = (EElementalyType)j;
                if(element == EElementalyType.Physical)
                {
                    element = EElementalyType.Nature;
                }

                enemyList[i * ((int)EElementalyType.AmountOfElements - 1) + j] = Resources.Load("Enemies/" + enemy.ToString() + "/" + enemy.ToString() + element.ToString()) as GameObject;
            }
        }
    }



    // Start is called before the first frame update
    private void Start()
    {
        list = DunguonSpawner.instance.roomRef;
        for (int i = 0; i < list.Count; i++)
        {
            roomList.Add(DunguonSpawner.instance.roomRef[i].GetComponent<Room>());
            Debug.Log(CalculatePoints(roomList[i]));
            roomList[i].roomSpawnPoints = CalculatePoints(roomList[i]);
            if (roomList[i].roomType != RoomType.Start)
            {
                SpawnEnemies(ref roomList[i].roomSpawnPoints, DunguonSpawner.instance.roomRef[i]);
            }
        }
    }

    int CalculatePoints(Room room)
    {
        int points = room.roomSpawnPoints;
        points = (int)room.Size.x * (int)room.Size.y;
        points = (int)(points * GetRoomDifficulityMultiplier(room.roomType));

        return points;
    }

    float GetRoomDifficulityMultiplier(RoomType type)
    {
        switch (type)
        {
            case RoomType.Easy:
                return 0.5f;
            case RoomType.Normal:
                return 1f;
            case RoomType.Hard:
                return 1.5f;
        }
        return 1f;
    }

    void SpawnEnemies(ref int maxCost, GameObject room)
    {
        
        BoxCollider spawnArea = room.transform.Find("Floor").GetComponent<BoxCollider>();
       
        int maxEnemyCost = GetMaxEnemyCost(floors[currentFloor].enemiesAllowedToSpawn, maxCost);

        while (maxCost > 0)
        {
            
            maxEnemyCost = GetMaxEnemyCost(floors[currentFloor].enemiesAllowedToSpawn, maxCost);
            Vector3 enemyPos = Vector3.zero;
            EnemyType enemyType = EnemyType.AmountOfEnemies;

            while (enemyType == EnemyType.AmountOfEnemies)
            {
                ////Debug.Log("Spawn");
                int pickEnemy = Random.Range(0, floors[currentFloor].enemiesAllowedToSpawn.Length);
                Debug.Log(pickEnemy);
                if (GetEnemyCost(floors[currentFloor].enemiesAllowedToSpawn[pickEnemy]) <= maxEnemyCost)
                {


                    Debug.Log(floors[currentFloor].enemiesAllowedToSpawn[pickEnemy]);
                    enemyType = floors[currentFloor].enemiesAllowedToSpawn[pickEnemy];
                    break;
                }
            }

            maxCost -= GetEnemyCost(enemyType);


            //enemyPos = RandomPointInBounds(spawnArea.bounds);
            enemyPos = room.transform.position;
            enemyPos.y = 2;




            GameObject tempEnemy = MonoBehaviour.Instantiate(enemyList[(int)enemyType * ((int)EElementalyType.AmountOfElements - 1) + RandomElement(floors[currentFloor])], enemyPos, transform.rotation) as GameObject;
            //tempEnemy.transform.position = enemyPos;
        }
    }

    int GetEnemyCost(EnemyType enemy)
    {
        switch (enemy)
        {
            case EnemyType.Golem:
                return 5;
            case EnemyType.Cactus:
                return 4;
            case EnemyType.PlantMonster:
                return 3;
            case EnemyType.Ghost:
                return 3;
            case EnemyType.Bat:
                return 1;
            case EnemyType.Knight:
                return 2;
            case EnemyType.Skeleton:
                return 1;
        }
        return 0;
    }


    public static Vector3 RandomPointInBounds(Bounds bounds)
    {

        return new Vector3(
            Random.Range(bounds.min.x + 1, bounds.max.x - 1),
            Random.Range(bounds.min.y + 1, bounds.max.y - 1),
            Random.Range(bounds.min.z + 1, bounds.max.z - 1)
        );
    }

    int RandomElement(FloorData elements)
    {
        int i = Random.Range(0, elements.floorTypes.Length);
        return (int)elements.floorTypes[i];
    }

    int GetMaxEnemyCost(EnemyType[] enemies, int maxCost)
    {
        int maxEnemyCost = 0;
        foreach (EnemyType enemy in floors[currentFloor].enemiesAllowedToSpawn)
        {
            int tempCost = GetEnemyCost(enemy);
            if (tempCost <= maxCost)
            {
                if (tempCost > maxEnemyCost)
                {
                    maxEnemyCost = tempCost;
                }
            }
        }
        return maxEnemyCost;
    }

}
