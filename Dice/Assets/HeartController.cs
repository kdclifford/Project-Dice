using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    private Text healthText;
    //[Range(0, 100)]
    public Image image;
    private Health health;
    private Scene currentScene;

    private void Start()
    {
        healthText = transform.GetChild(0).gameObject.GetComponent<Text>();
        image = GetComponent<Image>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene() != currentScene)
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

            healthText.text = health.GetHealth().ToString() + "%";
        image.material.SetFloat("_Health", 0.7f * health.GetHealth());
    }

}
