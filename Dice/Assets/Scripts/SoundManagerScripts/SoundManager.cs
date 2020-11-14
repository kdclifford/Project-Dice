using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Stores all the sound clips   
    [Header("Loaded Sounds"), Tooltip("Remember to save all your changes")]
    public Sound[] soundClips;

    //Makes sure there is only one instace of the audio manager
    public static SoundManager instance;

    //Checks the current scene used to change the music depending on the scene
    private string currentScene = "Null";

    //Checks for an instance of SoundManager in current scene
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //Used to check and change the audio depending on the scene
    private void Update()
    {
        if (currentScene != SceneManager.GetActiveScene().name)
        {
            currentScene = SceneManager.GetActiveScene().name;
            BackGroundMusic();
        }
    }


    //Called when the scene changes
    public void BackGroundMusic()
    {
        if (currentScene == "Menu")
        {
            Play("Menu Music 2", gameObject);

        }
        else
        {
            Play("Castle Music", gameObject);
        }
    }

    //Plays Sound if sound exists
    public void Play(string name, GameObject agent)
    {
        AudioSource agentAudio;
        if (agent.GetComponent<AudioSource>() == null)
        {
            agentAudio = agent.AddComponent<AudioSource>();
        }
        else
        {
            agentAudio = agent.GetComponent<AudioSource>();
        }

        Sound s = Array.Find(soundClips, Sound => Sound.name == name);
        if (s == null)
        {
            s = soundClips[0];
        }

        agentAudio.clip = s.clip;
        agentAudio.loop = s.loop;
        agentAudio.volume = s.volume;
        agentAudio.pitch = s.pitch;
        if (s.sound3D)
        {
            agentAudio.spatialBlend = 1;
        }
        else
        {
            agentAudio.spatialBlend = 0;
        }

        agentAudio.Play();
    }

    //Will play a place holder sound for testing purposes
    public void Play(GameObject agent)
    {
        Play(soundClips[0].name, agent);
        Debug.Log("Place Holder sound being used");
    }
}
