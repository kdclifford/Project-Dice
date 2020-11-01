using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int CurrentHearts = 7;
    [SerializeField]
    public GameObject[] HPUIIcons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            playerHit();
        }
        
    }

    void playerHit()
    {
        HPUIIcons[CurrentHearts].gameObject.SetActive(false);
        CurrentHearts--;
    }
}
