using UnityEngine.Audio;
using System.Xml; //Needed for XML functionality
using UnityEngine;

//Class used to store all sound variables
[System.Serializable]
public class Sound
{
    public string name; //Clip Name
    public AudioClip clip; //Sound Clip
    public bool loop; //Loop Sound
    public bool sound3D;
    [Range(0f, 1f)]
    public float volume = 0;
    [Range(0f, 3f)]
    public float pitch = 0;
    public EAudioGroups audioGroup;

    private  string location;
    //[SerializeField]
    private string audioMixerName = "";

    public Sound()
    {
    }

    public Sound(AudioClip audio)
    {
        //sound.clip = (AudioClip)Resources.Load("Sounds/" + obj.name);
        clip = audio;
        name = audio.name;
        volume = 1;
        pitch = 1;
        audioGroup = EAudioGroups.Master;
    }

    public Sound(XmlNode curItemNode)
    {
        name = curItemNode["name"].InnerText;

        if (curItemNode["loop"].InnerText == "true")
        {
            loop = true;
        }
        else
        {
            loop = false;
        }

        if (curItemNode["sound3D"].InnerText == "true")
        {
            sound3D = true;
        }
        else
        {
            sound3D = false;
        }

        XmlNode clipNode = curItemNode.SelectSingleNode("clip");
        location = clipNode["name"].InnerText;

        clip = (AudioClip)Resources.Load("Sounds/" + location);

        volume = float.Parse( curItemNode["volume"].InnerText);

        pitch = float.Parse(curItemNode["pitch"].InnerText);

        audioMixerName = curItemNode["audioGroup"].InnerText;

        audioGroup = (EAudioGroups)System.Enum.Parse(typeof(EAudioGroups), audioMixerName);

        //audioMixer = master.FindMatchingGroups(audioMixerName)[0];

    }

    public string GetGroupMixerName()
    {
        return audioMixerName;
    }

}

public enum EAudioGroups
{
    Master = 0,
    Music = 1,
    Projectiles = 2,
    Misc = 3,
}