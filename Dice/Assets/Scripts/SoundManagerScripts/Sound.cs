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
    private  string location;

    public Sound()
    {
    }

    public Sound(XmlNode curItemNode)
    {
        name = curItemNode["name"].InnerText;

        if (curItemNode["loop"].InnerText == "true")
        {
            loop = true;
        }

        if (curItemNode["sound3D"].InnerText == "true")
        {
            loop = true;
        }

        XmlNode clipNode = curItemNode.SelectSingleNode("clip");
        location = clipNode["name"].InnerText;

        clip = (AudioClip)Resources.Load("Sounds/" + location);

        volume = float.Parse( curItemNode["volume"].InnerText);

        pitch = float.Parse(curItemNode["pitch"].InnerText);

    }
}
