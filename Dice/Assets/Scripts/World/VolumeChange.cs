using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Audio;
using UnityEngine;

public class VolumeChange : MonoBehaviour
{
    [SerializeField]
    TextMeshPro volumeText;
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol")) { volumeText.text = PlayerPrefs.GetInt("MasterVol").ToString(); }
        if (PlayerPrefs.HasKey("MusicVol")) { volumeText.text = PlayerPrefs.GetInt("MusicVol").ToString(); }
        if (PlayerPrefs.HasKey("MiscVol")) { volumeText.text = PlayerPrefs.GetInt("MiscVol").ToString(); }
        if (PlayerPrefs.HasKey("ProjectileVol")) { volumeText.text = PlayerPrefs.GetInt("ProjectileVol").ToString(); }
        audioMixer = (AudioMixer)Resources.Load("Sounds/MasterMixer");
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
            // AudioListener.volume = currentVolume * 0.1f;
            //audioMixer.SetFloat(transform.parent.name, Mathf.Log10(currentVolume * 0.1f) * 20);
            audioMixer.SetFloat(transform.parent.name, Mathf.Log10(Mathf.Clamp(currentVolume * 0.1f, 0.0001f, 1f)) * 20);
            volumeText.text = currentVolume.ToString();
            PlayerPrefs.SetInt(transform.parent.name, currentVolume);
            return currentVolume;
        }
        else if (gameObject.name == "VolumeDown" && currentVolume > 0)
        {
            currentVolume--;
            //AudioListener.volume = currentVolume * 0.1f;
            audioMixer.SetFloat(transform.parent.name, Mathf.Log10(Mathf.Clamp(currentVolume * 0.1f, 0.0001f, 1f)) * 20);
            volumeText.text = currentVolume.ToString();
            PlayerPrefs.SetInt(transform.parent.name, currentVolume);
            return currentVolume;
        }

        return currentVolume;
    }
}
