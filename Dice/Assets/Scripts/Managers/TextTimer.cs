using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTimer : MonoBehaviour
{
    public Vector3 textOffset = new Vector3(0, 0.2f, 0);
    public Vector3 randomOffset = new Vector3(0.5f, 0, 0);
    public void Start()
    {
        transform.localPosition += textOffset;
        transform.localPosition += new Vector3(Random.Range(-randomOffset.x, randomOffset.x),
            Random.Range(-randomOffset.y, randomOffset.y),
            Random.Range(-randomOffset.z, randomOffset.z));
    }

    public void DestroyText()
    {
        Destroy(gameObject);
    }
}
