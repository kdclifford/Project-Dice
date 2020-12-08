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
    private SpellList spellList;

    private void Start()
    {
        soundManager = SoundManager.instance;
        textManager = TextManager.instance;
        textPrefab = (GameObject)Resources.Load("Fonts/Text");
        playerAnimations = GetComponent<PlayerAnimations>();
        agentHealth = GetComponent<Health>();
        uIManager = UIManager.instance;
        spellList = SpellList.instance;
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
                ShowFloatingText(other.gameObject.GetComponent<SpellIndex>().spellIndex);
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

                // playerAnimations.UpdateHeartUI();
                textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
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


                Destroy(other.gameObject);

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
}
