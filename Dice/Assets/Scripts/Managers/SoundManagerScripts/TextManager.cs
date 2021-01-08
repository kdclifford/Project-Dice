using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Managers all Hit text for player and enemies
public class TextManager : MonoBehaviour
{
    [Tooltip("Fonts used for the Player/Emeny Projectile Collisions")]
    [Header("Hit Text Fonts")]
    public Font[] fonts;

    public static TextManager instance;
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
    }


    //Gets the text that is going to be used
    public string SelectText()
    {
        string[] text = new string[] { "WHAAM", "PING", "KTANG", "BOP", "SOCK", "BAM!!!", "BOOM", "OOF" };
        int i = Random.Range(0, text.Length);
        return text[i];
    }

    //Returns random number in the range of font array
    //the index must be returned beause fonts act weird when being set during runtime
   public int SelectFont()
    {
        return Random.Range(0, fonts.Length);
    }

    //Gets font from the fonts array depending on index
    public Font GetFont(int index)
    {
        if(index > fonts.Length)
        {
            index = fonts.Length - 1;
        }
        else if(index < 0)
        {
            index = 0;
        }
        return fonts[index];
    }


}
