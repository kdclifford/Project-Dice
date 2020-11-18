using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private GameObject textPrefab;
    private SoundManager soundManager;
    private TextManager textManager;
    private Color textColour;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        textManager = soundManager.GetComponent<TextManager>();
        textPrefab = (GameObject)Resources.Load("Fonts/Text");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Equipped")
        {
            Destroy(other.gameObject);
            textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
            ShowFloatingText();
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
        GetComponent<EnemyAi>().health -= 25;
    }
}
