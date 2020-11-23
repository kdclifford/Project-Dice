using System.Collections;
using System.Collections.Generic;
using Button.Utils;
using TMPro;
using UnityEngine;

public class VolumeOption : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro volume;
    [SerializeField]
    private GameObject volumeUP;
    [SerializeField]
    private GameObject volumeDown;
    private float currentVolume = 10.0f;
    public EControllerType controllerType;

    // Start is called before the first frame update    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if(volumeDownCol.gameObject.tag == "Player")
        //{
        //
        //}
    }
}
