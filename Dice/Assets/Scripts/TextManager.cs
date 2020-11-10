using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public Font[] fonts;
   

    public string SelectText()
    {
        string[] text = new string[] { "WHAAM", "PING", "KTANG", "BOP", "SOCK", "BAM!!!", "BOOM", "OOF" };
        int i = Random.Range(0, text.Length);

        return text[i];
    }

   public int SelectFont()
    {
        int i = Random.Range(0, fonts.Length);

        return i;
    }

    public Font GetFont(int i)
    {
        return fonts[i];
    }


}
