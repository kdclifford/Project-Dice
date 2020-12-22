﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionResolution : MonoBehaviour
{
    private GameObject textPrefab;
    private SoundManager soundManager;
    private TextManager textManager;

    private PlayerAnimations playerAnimations;
    private Health agentHealth;
    private UIManager uIManager;
    private SpellList spellList;
    private float maxAoeTime = 0.6f;
    public float aoeBurnTime = 0;

    private void Start()
    {
        soundManager = SoundManager.instance;
        textManager = TextManager.instance;
        textPrefab = (GameObject)Resources.Load("Fonts/Text");
        playerAnimations = GetComponent<PlayerAnimations>();
        agentHealth = GetComponent<Health>();
        uIManager = UIManager.instance;
        spellList = SpellList.instance;
        aoeBurnTime = maxAoeTime;
    }

    private void Update()
    {
        aoeBurnTime -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 16)
        {
            ChekcForCollisions(other.gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 16)
        {
            if (aoeBurnTime <= 0)
            {
                ChekcForCollisions(other.gameObject);
                aoeBurnTime = maxAoeTime;
            }
        }
    }
    


    void ShowFloatingText(ESpellEnum projectile)
    {
        GameObject text = Instantiate(textPrefab, transform.position, textPrefab.transform.rotation) as GameObject;
        TextMesh textMesh = text.GetComponent<TextMesh>();
        int i = textManager.SelectFont();
        textMesh.font = textManager.GetFont(i);
        text.GetComponent<MeshRenderer>().material = textManager.GetFont(i).material;
        textMesh.text = textManager.SelectText();
        text.GetComponent<TextMesh>().color = spellList.spells[(int)projectile].castingColour;
        //textMesh.color = Color.white;

        GetComponent<Health>().RemoveHealth();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (aoeBurnTime <= 0)
        {
            if (transform.tag == "Enemy")
            {
                if (other.gameObject.tag == "Equipped")
                {

                    ShowFloatingText(other.gameObject.GetComponent<SpellIndex>().spellIndex);
                    aoeBurnTime = maxAoeTime;
                }
            }
            else if (transform.tag == "Player")
            {
                if (other.gameObject.tag == "EnemyProjectile")
                {
                    uIManager.RemoveHeart();
                    ShowFloatingText(other.gameObject.GetComponent<SpellIndex>().spellIndex);

                    if (agentHealth.GetHealth() <= 0)
                    {
                        Vector2 death = new Vector2(other.gameObject.transform.forward.x, other.gameObject.transform.forward.z);
                        death.Normalize();
                        playerAnimations.deathDirection = death;
                        playerAnimations.DeathAnimation();
                        soundManager.Play(ESoundClipEnum.PlayerDeath, gameObject);

                    }
                    else
                    {
                        soundManager.Play(ESoundClipEnum.PlayerHit, gameObject);
                    }
                    aoeBurnTime = maxAoeTime;

                    // Destroy(other.gameObject);

                }
            }
        }

    }   

    public void ChekcForCollisions(GameObject collider)
    {
        if (transform.tag == "Enemy")
        {
            if (collider.tag == "Equipped")
            {
                //Destory object and show text with 
                if (collider.layer != 16)
                {
                    Destroy(collider);
                }

                ShowFloatingText(collider.GetComponent<SpellIndex>().spellIndex);
            }
        }
        else if (transform.tag == "Player")
        {
            if (collider.tag == "EnemyProjectile")
            {
                //playerHealth.playerHit();

                //Work out the direction the projectile came from
                //Vector2 death = new Vector2(
                //  other.gameObject.GetComponent<Rigidbody>().velocity.x, other.gameObject.GetComponent<Rigidbody>().velocity.z);

                // playerAnimations.UpdateHeartUI();
                uIManager.RemoveHeart();
                ShowFloatingText(collider.GetComponent<SpellIndex>().spellIndex);

                if (agentHealth.GetHealth() <= 0)
                {
                    Vector2 death = new Vector2(collider.transform.forward.x, collider.transform.forward.z);
                    death.Normalize();
                    playerAnimations.deathDirection = death;
                    playerAnimations.DeathAnimation();
                    soundManager.Play(ESoundClipEnum.PlayerDeath, gameObject);

                }
                else
                {
                    soundManager.Play(ESoundClipEnum.PlayerHit, gameObject);
                }


                Destroy(collider);

            }
        }
    }

}
    


