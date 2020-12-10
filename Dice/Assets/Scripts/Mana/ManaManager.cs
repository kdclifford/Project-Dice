using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
   [ SerializeField]
    private int fire, water, electricity, rock, wind;
   
    public int Fire
    {
        get{ return fire;}
        set { fire = value; }
    }

    public int Water
    {
        get { return water; }
        set { water = value; }
    }

    public int Electricity
    {
        get { return electricity; }
        set { electricity = value; }
    }

    public int Rock
    {
        get { return rock; }
        set { rock = value; }
    }

    public int Wind
    {
        get { return wind; }
        set { wind = value; }
    }

    public int AllMana { get { return fire + water + wind + electricity + rock; } }


    public static ManaManager instance;

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
