using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageCreate : MonoBehaviour
{
    public Texture2D oldTexture;
    public Texture2D NewTexture;
    public string fileName;

    private Color oldColour;

    //public GameObject bodyObject;
    //public GameObject eyeObject;

    // Start is called before the first frame update
    public void Combine()
    {
        // oldColour = eyeObject.GetComponent<SpriteRenderer>().color;
        //NewTexture = new Texture2D(oldTexture.width, oldTexture.height);
         Gradient();
        //BlackandWhite();
    }


    public void Gradient()
    {
        //BodyColour.a = 1f;
        //EyeColour.a = 1f;

        int startX = 0;
        int startY = 0;

        float i = 1.0f / oldTexture.height;

        for (int x = startX; x < oldTexture.width; x++)
        {

            for (int y = startY; y < oldTexture.height; y++)
            {
                Color bgColor = oldTexture.GetPixel(x, y);

                float test = (float)y / oldTexture.height;

                if (bgColor.a != 0)
                {
                    bgColor = Color.Lerp(Color.black, Color.white, i * y);
                    bgColor.a = Mathf.Lerp(0, 1, i * y);
                }


                NewTexture.SetPixel(x, y, bgColor);
                
               



                //Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

            }
        }

        
       NewTexture.Apply();


        byte[] itemBGBytes = NewTexture.EncodeToPNG();
       // File.WriteAllBytes("/Assets/", itemBGBytes);
        File.WriteAllBytes(Application.dataPath + "/" + fileName + ".png", itemBGBytes);

    }


    public void BlackandWhite()
    {
        //BodyColour.a = 1f;
        //EyeColour.a = 1f;
        //NewTexture = oldTexture;
        int startX = 0;
        int startY = 0;

        float i = 1.0f / oldTexture.height;

        for (int x = startX; x < oldTexture.width; x++)
        {

            for (int y = startY; y < oldTexture.height; y++)
            {
                Color bgColor = oldTexture.GetPixel(x, y);

                float test = (float)y / oldTexture.height;

                if (bgColor.r > 0 && bgColor.g > 0 && bgColor.b > 0)
                {
                    bgColor.r = 1;
                    bgColor.a = 0;
                }
                else
                {
                    bgColor.a = 1;
                }


                NewTexture.SetPixel(x, y, bgColor);





                //Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

            }
        }


        NewTexture.Apply();


        byte[] itemBGBytes = NewTexture.EncodeToPNG();
        // File.WriteAllBytes("/Assets/", itemBGBytes);
        File.WriteAllBytes(Application.dataPath + "/SavedScreen.png", itemBGBytes);

    }


}
