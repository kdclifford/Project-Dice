using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject leftHeart;
    public GameObject rightHeart;
    public GameObject boarder;
    public GameObject canvas;

    private Health playerHealth;
    private GameObject[] hearts;
    private GameObject[] shield;
    private int maxHearts;
    private int maxShields;

    public void AddHeart(int currentHealth)
    {


    }

    public void RemoveHeart()
    {
        if (playerHealth.GetShield() > 0)
        {
            shield[playerHealth.GetShield()].gameObject.SetActive(false);           
            return;
        }

        if (playerHealth.GetHealth() > 0)
        {
            hearts[playerHealth.GetHealth()].gameObject.GetComponent<Animator>().SetInteger("Health", 1);
            /// health.currentHealth--;
        }
    }

    public void AddUIHeart()
    {
        hearts[playerHealth.GetHealth()].gameObject.GetComponent<Animator>().SetInteger("Health", 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        maxHearts = (int)playerHealth.maxHealth;
        maxShields = (int)playerHealth.maxShields;
        hearts = new GameObject[maxHearts + 1];
        shield = new GameObject[maxShields + 1];
        Vector3 heartsPosLeft = new Vector3(-1, 0);
        Vector3 heartsPosRight = new Vector3(-1, 0);

        for (int i = 0; i < maxHearts + 1; i++)
        {
       heartsPosLeft.x = (-122 + (37.5f * i) / ScreenScale.x);
            hearts[i] = Instantiate(leftHeart, boarder.transform.position, Quaternion.identity, canvas.transform) as GameObject;
            hearts[i].GetComponent<RectTransform>().localPosition += heartsPosLeft;
            //hearts[i].GetComponent<RectTransform>().position +=  heartsPos;
            heartsPosRight.x = (-97 + (37.5f * i) / ScreenScale.x);
            i++;
            hearts[i] = Instantiate(rightHeart, boarder.transform.position, Quaternion.identity, canvas.transform) as GameObject;
            hearts[i].GetComponent<RectTransform>().localPosition += heartsPosRight;
        }
    }

  

    private CanvasScaler canvasScaler;
    private Vector2 ScreenScale
    {
        get
        {
            if (canvasScaler == null)
            {
                canvasScaler = canvas.GetComponent<CanvasScaler>();
            }

            if (canvasScaler)
            {
                return new Vector2(canvasScaler.referenceResolution.x / Screen.width, canvasScaler.referenceResolution.y / Screen.height);
            }
            else
            {
                return Vector2.one;
            }
        }
    }
}
