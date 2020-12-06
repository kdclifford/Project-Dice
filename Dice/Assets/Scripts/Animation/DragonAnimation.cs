using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimation : MonoBehaviour
{
    public GameObject mouthPos;
    public GameObject attackPrefab;
    public Vector3 mouthOffset;

    public void SpawnBreath()
    {
        GameObject attack = Instantiate(attackPrefab, mouthPos.transform.position + mouthOffset, Quaternion.Euler(0, 180, 0), mouthPos.transform) as GameObject;
    }

}
