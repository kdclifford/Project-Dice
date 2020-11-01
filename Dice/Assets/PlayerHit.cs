using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private SoundManager soundManager;
    public GameObject textPrefab;
    private Color textColour;

    public Font[] fonts;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyProjectile")
        {
            transform.parent.GetComponent<PlayerHealth>().playerHit();
            Vector2 death = new Vector2(
              other.gameObject.GetComponent<Rigidbody>().velocity.x, other.gameObject.GetComponent<Rigidbody>().velocity.z);
            death.Normalize();
            transform.parent.GetComponent<PlayerHealth>().deathDirection = -death;
            Destroy(other.gameObject);

            textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
            ShowFloatingText();
            if (transform.parent.GetComponent<PlayerHealth>().CurrentHearts <= -1)
            {
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<Collider>());

               
                soundManager.Play("Player Death", transform.parent.gameObject);
            }
            else
            {

                soundManager.Play("Player Hit", transform.parent.gameObject);
            }
           
        }
    }

    void ShowFloatingText()
    {
        GameObject text = Instantiate(textPrefab, transform.position, textPrefab.transform.rotation) as GameObject;
        int i = SelectFont();
        text.GetComponent<TextMesh>().font = fonts[i];
        text.GetComponent<MeshRenderer>().material = fonts[i].material;
        text.GetComponent<TextMesh>().text = SelectText();
        text.GetComponent<TextMesh>().color = textColour;
    }

    string SelectText()
    {
        string[] text = new string[] { "WHAAM", "PING", "KTANG", "BOP", "SOCK", "BAM!!!", "BOOM", "OOF" };
        int i = Random.Range(0, text.Length);

        return text[i];
    }

    int SelectFont()
    {       
        int i = Random.Range(0, fonts.Length);

        return i;
    }
}
