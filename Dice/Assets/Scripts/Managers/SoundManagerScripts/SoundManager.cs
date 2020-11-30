using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Stores all the sound clips   
    [Header("Loaded Sounds"), Tooltip("Remember to save all your changes")]
    public Sound[] soundClips;
    [HideInInspector]
    public AudioMixer audioMixer;

    //Makes sure there is only one instace of the audio manager
    public static SoundManager instance;

    //Checks the current scene used to change the music depending on the scene
    private int currentScene = -1;
    private int oldScene = -1;

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
        //audioMixer.SetFloat("MasterVol", volume);
        if (currentScene != SceneManager.GetActiveScene().buildIndex)
        {
            oldScene = currentScene;
            currentScene = SceneManager.GetActiveScene().buildIndex;
            BackGroundMusic();
        }
    }


    //Called when the scene changes
    public void BackGroundMusic()
    {
        if (currentScene == 0 || currentScene == 1)
        {
            if (oldScene != 0 && oldScene != 1)
            {
                Play(SoundClipEnum.MenuMusic, gameObject);
            }

        }
        else
        {
            Play(SoundClipEnum.CastleMusic, gameObject);
        }
    }

    //Plays Sound if sound exists
    public void Play(SoundClipEnum name, GameObject agent)
    {
        AudioSource agentAudio = FindAudio(agent, name);
        agentAudio.Play();
    }

    //Will play a place holder sound for testing purposes
    public void Play(GameObject agent)
    {
        Play((SoundClipEnum)1, agent);
        Debug.Log("Place Holder sound being used");
    }

    //Will play a sound clip once, at the postion it was called at
    public void PlayOnceAtPoint(SoundClipEnum clipEnum , GameObject agent)
    {
        AudioSource agentAudio = FindAudio(agent, clipEnum);
        agentAudio.PlayOneShot(agentAudio.clip);
        
    }

    //Play another Sound clip the audio source will be destroyed when clip is finished
    void PlayAndDestroy(SoundClipEnum clipEnum, GameObject agent)
    {
        AudioSource agentAudio = agent.AddComponent<AudioSource>();
        Play(clipEnum, agent);
       Destroy(agentAudio, agentAudio.clip.length);
    }


    //Checks for an AudioSource
    private AudioSource CheckAudioSource(GameObject agent)
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

        return agentAudio;
    }

    //Find audio clip in array
    private AudioSource FindAudio(GameObject agent, SoundClipEnum clipName)
    {
        AudioSource agentAudio = CheckAudioSource(agent);
        // Sound s = Array.Find(soundClips, Sound => Sound.name == clipName);
       // SoundClipEnum clipcheck = (SoundClipEnum)Enum.Parse(typeof(SoundClipEnum), "OIOI", true);
        int a = 4;
        Sound s = soundClips[(int)clipName];
        if (s == null)
        {
            s = soundClips[0];
        }       

        agentAudio.clip = s.clip;
        agentAudio.loop = s.loop;
        agentAudio.volume = s.volume;
        agentAudio.pitch = s.pitch;
        agentAudio.outputAudioMixerGroup = audioMixer.FindMatchingGroups(s.GetGroupMixerName())[(int)s.audioGroup];

        if (s.sound3D)
        {
            agentAudio.spatialBlend = 1;
        }
        else
        {
            agentAudio.spatialBlend = 0;
        }

        return agentAudio;
    }


}


