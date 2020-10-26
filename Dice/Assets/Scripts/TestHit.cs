using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHit : MonoBehaviour
{
    public GameObject textPrefab;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color textColour;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Equipped")
        {
            Destroy(other.gameObject);
            textColour = other.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
            ShowFloatingText();
        }
    }

    void ShowFloatingText()
    {
        GameObject text = Instantiate(textPrefab, transform.position, textPrefab.transform.rotation) as GameObject;
        text.GetComponent<TextMesh>().text = Random.Range(10, 50).ToString();
        text.GetComponent<TextMesh>().color = textColour;
        
    }

    
}
