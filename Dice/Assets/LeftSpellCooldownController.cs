using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LeftSpellCooldownController : MonoBehaviour
{
    private Text timerText;
    private Image timerImage;
    private string timerString;


    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponent<Image>();
        timerImage.fillAmount = 0;

        timerText = transform.GetChild(0).GetComponent<Text>();
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float timer = PlayerController.instance.currLTFireCooldown;
        if(timer < 0)
        {
            timerString = "";
        }
        else
        {
            timerString = ((int)timer).ToString();
        }
        timerImage.fillAmount = PlayerController.instance.currLTFireCooldown / PlayerController.instance.MaxLTFireCooldown;
        timerText.text = timerString;
    }
}
