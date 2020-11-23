using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    [SerializeField]
    TextMeshPro volumeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ChangeVolume(int volume)
    {
        int currentVolume = volume;
        if(gameObject.name == "VolumeUp" && currentVolume < 10)
        {
            currentVolume++;
            AudioListener.volume = currentVolume;
            volumeText.text = currentVolume.ToString();
            PlayerPrefs.SetInt("Volume", currentVolume);
            return currentVolume;
        }
        else if (gameObject.name == "VolumeDown" && currentVolume > 0)
        {
            currentVolume--;
            AudioListener.volume = currentVolume;
            volumeText.text = currentVolume.ToString();
            PlayerPrefs.SetInt("Volume", currentVolume);
            return currentVolume;
        }

        return currentVolume;
    }
}
