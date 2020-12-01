using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public static SpellList instance;
    
    public List<GameObject> spells;
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
}
