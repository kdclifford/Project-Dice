using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    private Text healthText;
    private Image healthBar;
    private Health health;

    private void Start()
    {
        health = PlayerController.instance.gameObject.GetComponent<Health>();
        healthBar = transform.GetChild(1).gameObject.GetComponent<Image>();
        healthBar.fillAmount = health.GetHealth() / health.maxHealth;
        healthText = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        healthText.text = health.GetHealth().ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health.GetHealth() / health.maxHealth;
        healthText.text = health.GetHealth().ToString();
    }
}
