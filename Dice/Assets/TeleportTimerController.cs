using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TeleportTimerController : MonoBehaviour
{
    private Text timerText;
    private Image timerImage;
    private string timerString;
    private Teleport teleport;

    // Start is called before the first frame update
    void Start()
    {
        teleport = PlayerController.instance.gameObject.GetComponent<Teleport>();
        timerImage = GetComponent<Image>();
        timerImage.fillAmount = 0;

        timerText = transform.GetChild(0).GetComponent<Text>();
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float timer = teleport.maxCooldownTimer;
        if (timer < 0)
        {
            timerString = "";
        }
        else
        {
            //timerString = ((int)timer).ToString();
        }
        timerImage.fillAmount = teleport.cooldownTimer / teleport.maxCooldownTimer;
        timerText.text = timerString;
    }
}
