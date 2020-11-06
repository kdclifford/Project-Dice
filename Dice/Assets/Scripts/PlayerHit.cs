using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private SoundManager soundManager;
    public GameObject textPrefab;
    private Color textColour;
    private TextManager textManager;


    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        textManager = soundManager.GetComponent<TextManager>();
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
        int i = textManager.SelectFont();
        text.GetComponent<TextMesh>().font = textManager.GetFont(i);
        text.GetComponent<MeshRenderer>().material = textManager.GetFont(i).material;
        text.GetComponent<TextMesh>().text = textManager.SelectText();
        text.GetComponent<TextMesh>().color = textColour;
    }  
}
