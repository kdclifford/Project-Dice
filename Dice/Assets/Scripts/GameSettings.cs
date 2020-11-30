using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public EControllerType controllerType;
    public int controllerAmount = 0;
    public string[] controllerNames;
    public static GameSettings instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        controllerAmount = Input.GetJoystickNames().Length;
        controllerNames = Input.GetJoystickNames();
        if (controllerAmount > 0 && controllerNames[0] != "")
        {
            controllerType = EControllerType.Controller;
        }
        else
        {
            controllerType = EControllerType.Computer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
