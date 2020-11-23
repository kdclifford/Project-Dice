using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniamtionBallScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject ballPosistion;
    public float ballSpeed;
    public void ThrowBall()
    {
        GameObject throwBall = Instantiate(ballPrefab, ballPosistion.transform.position, Quaternion.identity) as GameObject;
        throwBall.GetComponent<Rigidbody>().AddForce(transform.forward * ballSpeed);
    }
}
