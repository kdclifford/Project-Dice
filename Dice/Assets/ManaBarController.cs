using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManaBarController : MonoBehaviour
{
    private Text manaText;
    private Image manaBar;
    private Mana mana;

    private void Start()
    {
        mana = Mana.instance;
        manaBar = transform.GetChild(1).gameObject.GetComponent<Image>();
        manaBar.fillAmount = mana.GetMana() / mana.maxMana;
        manaText = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        manaText.text = mana.GetMana().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.fillAmount = ((float)mana.GetMana()) / mana.maxMana;
        manaText.text = mana.GetMana().ToString();
    }
}
