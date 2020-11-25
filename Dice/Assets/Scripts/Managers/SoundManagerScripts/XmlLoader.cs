using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

public class XmlLoader : MonoBehaviour
{
    [Tooltip("Sounds that will be added to the Sound Manager component")]
    public Sound[] addedSounds;

   private XmlDocument xmlDoc;

    private SoundManager soundManager;

    private void Awake()
    {
        //Load Sound manager
        soundManager = gameObject.GetComponent<SoundManager>();


        //Load XML
        LoadXml();
    }

    public void LoadXml()
    {
        if (soundManager == null)
        {
            FindSoundManager();
        }
                //soundManager = gameObject.GetComponent<SoundManager>();

                soundManager.audioMixer = (AudioMixer)Resources.Load("Sounds/MasterMixer");
       

        if (soundManager.soundClips.Length == 0)
        {


            soundManager.soundClips = null;
            //if (AssetDatabase.IsValidFolder(Application.dataPath + "XML/SoundXml.xml"))
            //{
            //Array.Clear(soundManager.soundClips, 0, soundManager.soundClips.Length);
            TextAsset xmlData;
            if (xmlData = Resources.Load<TextAsset>("XML/SoundXml"))
            {


                xmlDoc = new XmlDocument();

                //if (Directory.Exists(Application.dataPath + "/Resources/XML/SoundXml.xml"))
                //{

                if (File.Exists(Application.dataPath + "/Resources/XML/SoundXml.xml"))
                {
                    xmlDoc.LoadXml(xmlData.text);

                    

                    XmlNodeList itemList = xmlDoc.SelectNodes("/ArrayOfSound/Sound");

                    int listSize = 0;

                    foreach (XmlNode item in itemList)
                    {
                        listSize++;
                    }

                    Sound[] temp = new Sound[listSize];

                    int currentItem = 0;
                    foreach (XmlNode item in itemList)
                    {
                        Sound soundData = new Sound(item);

                        temp[currentItem] = soundData;

                        currentItem++;

                        //Array.Clear(temp, 0, temp.Length);
                        //soundManager.soundClips[soundManager.soundClips.Length] = soundData;
                    }

                    soundManager.soundClips = temp;

                    // wait for the load and set your property
                    // soundManager.soundClips[0].clip = (AudioClip)Resources.Load("Sounds/403009__inspectorj__ui-confirmation-alert-b3");

                    //if (item.Parent.Attribute(“number”).Value == iteration.ToString())
                }
            }
            else
            {
                // throw new Exception("No xml File created");
            }
        }
    }


    void FindSoundManager()
    {
        soundManager = gameObject.GetComponent<SoundManager>();
    }

    public void SaveXML()
    {
       
            if (soundManager == null)
            {
                FindSoundManager();
            }

            if (soundManager.soundClips == null)
            {
                LoadXml();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Sound[]));
            FileStream stream = new FileStream(Application.dataPath + "/Resources/XML/SoundXml.xml", FileMode.Create);
            serializer.Serialize(stream, soundManager.soundClips);
            stream.Close();

            // Array.Clear(soundManager.soundClips, 0, soundManager.soundClips.Length);
        
    }

    public void AddSoundToList()
    {
        if (soundManager == null)
        {
            FindSoundManager();
        }

        if (soundManager.soundClips == null)
        {
            LoadXml();
        }


        if (!CheckForClip())
        {
            throw new Exception("Null clip");
        }

        if (CheckForDupes())
        {
            throw new Exception("Dupe Found Change name");
        }

        Sound[] temp;
        if (soundManager.soundClips != null)
        {
            temp = new Sound[soundManager.soundClips.Length + addedSounds.Length];
            soundManager.soundClips.CopyTo(temp, 0);

        }
        else
        {
            temp = new Sound[addedSounds.Length];
        }

        int i = 0;
        foreach (Sound s in addedSounds)
        {

            temp[temp.Length - (addedSounds.Length - i)] = s;
            i++;
        }
        soundManager.soundClips = temp;
        addedSounds = new Sound[0];
    }


    public void ClearAll()
    {
        FindSoundManager();
        soundManager.soundClips = new Sound[0];
        addedSounds = new Sound[0];
    }



    public void AddXML(AudioClip audio)
    {
       // Debug.Log(AssetDatabase.GetAssetPath(obj));        

        Sound sound = new Sound(audio);       

        Sound[] temp;
        if (addedSounds != null)
        {
            
            temp = new Sound[addedSounds.Length + 1];
            addedSounds.CopyTo(temp, 0);
            temp[temp.Length - 1] = sound;
        }
        else
        {
            temp = new Sound[1];
            temp[0] = sound;
        }
        

        
        addedSounds = temp;



    }



    //Check the same name hasent been added
    bool CheckForDupes()
    {
        foreach (Sound s in addedSounds)
        {
            if (s.name == "")
            {
                return true;
            }


            if (soundManager.soundClips != null)
            {
                for (int i = 0; i < soundManager.soundClips.Length; i++)
                {
                    if (soundManager.soundClips[i].name == s.name)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool CheckForClip()
    {
        foreach (Sound s in addedSounds)
        {
            if (s.clip != null)
            {
                return true;
            }
        }
        return false;
    }



}