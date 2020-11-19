using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResolution : MonoBehaviour
{
    private GameObject textPrefab;
    private SoundManager soundManager;
    private TextManager textManager;
    private Color textColour;

    private PlayerHealth playerHealth;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        textManager = soundManager.GetComponent<TextManager>();
        textPrefab = (GameObject)Resources.Load("Fonts/Text");
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.tag == "Enemy")
        {
            if (other.gameObject.tag == "Equipped")
            {
                Destroy(other.gameObject);
                textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
                ShowFloatingText();
            }
        }
        else if(transform.tag == "Player")
        {
            if (other.gameObject.tag == "EnemyProjectile")
            {               
                Vector2 death = new Vector2(
                  other.gameObject.GetComponent<Rigidbody>().velocity.x, other.gameObject.GetComponent<Rigidbody>().velocity.z);
                death.Normalize();
                
                Destroy(other.gameObject);
                textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
                ShowFloatingText();
                if (GetComponent<Health>().currentHealth <= 0)
                {
                    playerHealth.deathDirection = death;
                    soundManager.Play("Player Death", gameObject);
                }
                else
                {
                    soundManager.Play("Player Hit", gameObject);
                }

            }
        }        
    }

    void ShowFloatingText()
    {
        GameObject text = Instantiate(textPrefab, transform.position, textPrefab.transform.rotation) as GameObject;
        int i = textManager.SelectFont();
        text.GetComponent<TextMesh>().font = textManager.GetFont(i);
        text.GetComponent<MeshRenderer>().material = textManager.GetFont(i).material;
        text.GetComponent<TextMesh>().text = textManager.SelectText();
        text.GetComponent<TextMesh>().color = textColour;
        GetComponent<Health>().RemoveHealth();
    }
}
