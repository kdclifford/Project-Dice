using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCreate : MonoBehaviour
{
    public Texture2D oldTexture;
    public Texture2D NewTexture;

    private Color oldColour;

    //public GameObject bodyObject;
    //public GameObject eyeObject;

    // Start is called before the first frame update
    public void Combine()
    {
       // oldColour = eyeObject.GetComponent<SpriteRenderer>().color;
        NewTexture = MergeImage(oldTexture, NewTexture);
    }


    public Texture2D MergeImage(Texture2D OldTexture, Texture2D NewTexture)
    {
        //BodyColour.a = 1f;
        //EyeColour.a = 1f;

        int startX = 0;
        int startY = 0;

        for (int x = startX; x < OldTexture.width; x++)
        {

            for (int y = startY; y < OldTexture.height; y++)
            {
                Color bgColor = OldTexture.GetPixel(x, y);
                if(bgColor == Color.black )
                {
                    bgColor.a = 0;
                }


                NewTexture.SetPixel(x, y, bgColor);
                
               



                //Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

            }
        }

        NewTexture.Apply();
        return NewTexture;
    }




}
