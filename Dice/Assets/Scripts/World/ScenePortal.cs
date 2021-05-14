using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenePortal : MonoBehaviour
{
    [SerializeField]
    string PortalToSceneName;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * -0.5f);
    }

    public void TeleportToScene()
    {
        if(PortalToSceneName == "Quit")
        {
            Application.Quit();
        }

        levelManager.LoadLevel(PortalToSceneName);
    }
}
