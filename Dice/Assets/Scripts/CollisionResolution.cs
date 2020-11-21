using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResolution : MonoBehaviour
{
    private GameObject textPrefab;
    private SoundManager soundManager;
    private TextManager textManager;
    private Color textColour;

    private PlayerAnimations playerAnimations;
    private Health agentHealth;
    private UIManager uIManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        textManager = soundManager.GetComponent<TextManager>();
        textPrefab = (GameObject)Resources.Load("Fonts/Text");
        playerAnimations = GetComponent<PlayerAnimations>();
        agentHealth = GetComponent<Health>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.tag == "Enemy")
        {
            if (other.gameObject.tag == "Equipped")
            {
                //Destory object and show text with 
                Destroy(other.gameObject);
                textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
                ShowFloatingText(other.gameObject);
            }
        }
        else if (transform.tag == "Player")
        {
            if (other.gameObject.tag == "EnemyProjectile")
            {
                //playerHealth.playerHit();

                //Work out the direction the projectile came from
                //Vector2 death = new Vector2(
                //  other.gameObject.GetComponent<Rigidbody>().velocity.x, other.gameObject.GetComponent<Rigidbody>().velocity.z);
                agentHealth.RemoveHealth();
                // playerAnimations.UpdateHeartUI();
                uIManager.RemoveHeart();

                if (agentHealth.GetHealth() <= 0)
                {
                    Vector2 death = new Vector2(other.gameObject.transform.forward.x, other.gameObject.transform.forward.y);
                    death.Normalize();
                    playerAnimations.deathDirection = death;
                    playerAnimations.DeathAnimation();
                    soundManager.Play("Player Death", gameObject);
                }
                else
                {
                    soundManager.Play("Player Hit", gameObject);
                }
                

                Destroy(other.gameObject);
                textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
                ShowFloatingText(other.gameObject);

            }
        }
    }

    void ShowFloatingText(GameObject projectile)
    {
        GameObject text = Instantiate(textPrefab, transform.position, textPrefab.transform.rotation) as GameObject;
        int i = textManager.SelectFont();
        text.GetComponent<TextMesh>().font = textManager.GetFont(i);
        text.GetComponent<MeshRenderer>().material = textManager.GetFont(i).material;
        text.GetComponent<TextMesh>().text = textManager.SelectText();
        text.GetComponent<TextMesh>().color = projectile.GetComponent<ParticleSystem>().main.startColor.color;
        GetComponent<Health>().RemoveHealth();
    }
}
