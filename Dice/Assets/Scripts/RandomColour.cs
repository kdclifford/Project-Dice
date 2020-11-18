using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColour : MonoBehaviour
{
    private SkinnedMeshRenderer meshRenderer;
    public Color staffColour;
    public Color originalColour;
    float timer = 0;
    public float duration;
    private Color newColour;
    public bool lerpOn = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        originalColour = meshRenderer.material.color;
        LerpStaffColour();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.SetColor("_Color", newColour);

        if (lerpOn)
        {
            LerpStaffColour();
            timer += Time.deltaTime / duration;

            if (timer > 1)
            {
                //timer = 0;
                lerpOn = false;
                LerpStaffColour();
            }
        }
        else
        {
            LerpStaffColour();
            timer -= Time.deltaTime / duration;

            if (timer <= 0)
            {
                timer = 0;                
                LerpStaffColour();
            }
        }



    }

    void LerpStaffColour()
    {
        newColour = Color.Lerp(originalColour, staffColour, timer);
    }

   


}
