using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EditorMethods : Editor
{
   
     const string extension = ".cs";

    public static void WriteToEnum(string[] data , string name, string path)
    {
        using (StreamWriter file = File.CreateText(path + name + extension))
        {
            file.WriteLine("//DO NOT EDIT THIS SCRIPT");
            file.WriteLine("//This script is created by the unity editor to make looking for sound clips faster \n");
            file.WriteLine("public enum " + name + " \n{");

           
            for(int i = 0; i < data.Length; i++)
            { 
                string lineRep = data[i].ToString().Replace(" ", string.Empty);
                if (!string.IsNullOrEmpty(lineRep))
                {
                    file.WriteLine(string.Format("\t{0} = {1},",
                        lineRep, i));
                    //i++;
                }
            }

            file.WriteLine("\n}");
        }

        AssetDatabase.ImportAsset(path + name + extension);
    }
}

