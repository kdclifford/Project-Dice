using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public static Mana instance;
    public int maxMana = 100;
    public int currentMana;
    private float manaTimer = 0;
    public float startTime = 1;

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
    public void AddMana(int mana)
    {
        currentMana += mana;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void RemoveMana(int mana)
    {
        currentMana -= mana;
    }

    private void Update()
    {
        if (currentMana <= maxMana)
        {
            if (manaTimer <= 0)
            {

                currentMana += 2;
                if(currentMana > maxMana)
                {
                    currentMana = maxMana;
                }
                manaTimer = startTime;
            }
        }
        manaTimer -= Time.deltaTime;
    }


}
