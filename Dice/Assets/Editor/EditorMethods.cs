using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorMethods : Editor
{

    const string extension = ".cs";

    public static void WriteToEnum(string[] data, string name, string path)
    {

        if (name == "ESpellEnum" && File.Exists(path + name + extension))
        {
            int offset = 0;
            string[] newData = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                //for (int j = 0; j < data.Length; i++)
                //{
                if (data[i] != ((ESpellEnum)i - offset).ToString())
                {
                    newData[(newData.Length - 1) - offset] = data[i];
                    offset++;
                }
                else
                {
                    newData[i] = data[i - offset];
                }

            }
            data = newData;
        }


        using (StreamWriter file = File.CreateText(path + name + extension))
            {
                file.WriteLine("//DO NOT EDIT THIS SCRIPT");
                file.WriteLine("//This script is created by the unity editor to make looking for sound clips faster \n");
                file.WriteLine("public enum " + name + " \n{");

                for (int i = 0; i < data.Length; i++)
                {
                    string lineRep = data[i].ToString().Replace(" ", string.Empty);
                    if (!string.IsNullOrEmpty(lineRep))
                    {
                        file.WriteLine(string.Format("\t{0} = {1},", lineRep, i));
                        //i++;
                    }
                }

                file.WriteLine("    Size, \n");
                file.WriteLine("\n}");
            }

            AssetDatabase.ImportAsset(path + name + extension);
        }
    }

