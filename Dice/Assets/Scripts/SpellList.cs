using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellList : MonoBehaviour
{
    public static SpellList instance;

    // [HideInInspector]
    public SpellBase[] spells;

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

    private void OnEnable()
    {
        //FireBall fireBall = new FireBall();
        //Spark spark = new Spark();
        //Bubble bubble = new Bubble();

        InitializeSpells();
    }

    public void InitializeSpells()
    {
        spells = new SpellBase[] { new FireBall(), new Spark(), new Bubble() };

        foreach (SpellBase spell in spells)
        {
            spell.SetValues();
        }
    }
}
