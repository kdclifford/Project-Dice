using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public static Mana instance;
    private int maxMana = 100;
    public int currentMana;


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
        currentMana = maxMana;
    }

    public int GetMana()
    {
      return currentMana;
    }

    public void RemoveMana(int mana)
    {
        currentMana -= mana;
    }
}
