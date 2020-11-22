using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("UI Refereneces")]
    private Image LeftUIIcon;
    [SerializeField]
    private Image RightUIIcon;
    [SerializeField]
    private Image interactPopup;
    [SerializeField]
    private Image EquipPopup;

    public GameObject leftHeart;
    public GameObject rightHeart;
    public GameObject shieldPrefab;
    public GameObject boarder;
    public GameObject canvas;


    private Health playerHealth;
    private GameObject[] hearts;
    private GameObject[] shield;
    private int maxHearts;
    private int maxShields;



    public void RemoveHeart()
    {
        if (playerHealth.GetShield() > 0)
        {
            shield[playerHealth.GetShield()- 1].SetActive(false);
            // shield[playerHealth.GetShield() - 1].gameObject.GetComponent<Animator>().SetInteger("Health", 1);
            //playerHealth.SetShield(playerHealth.GetShield() - 1);
            return;
        }

        if (playerHealth.GetHealth() > 0)
        {
            hearts[playerHealth.GetHealth() - 1].gameObject.GetComponent<Animator>().SetInteger("Health", 1);
            /// health.currentHealth--;
        }
    }

    public void AddUIHeart()
    {
        hearts[playerHealth.GetHealth() - 1].gameObject.GetComponent<Animator>().SetInteger("Health", 0);
    }

    public void AddUIShield()
    {
        shield[playerHealth.GetShield()].SetActive(true);
        //shield[playerHealth.GetShield()].gameObject.GetComponent<Animator>().SetInteger("Health", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        maxHearts = (int)playerHealth.maxHealth;
        maxShields = (int)playerHealth.maxShields;
        hearts = new GameObject[maxHearts];
        shield = new GameObject[maxShields];
        Vector3 heartsPosLeft = new Vector3(-1, 0);
        Vector3 heartsPosRight = new Vector3(-1, 0);
        Vector3 shieldPosLeft = new Vector3(-1, 0);

        for (int i = 0; i < maxHearts; i++)
        {
            heartsPosLeft.x = (-122 + (37.5f * i));
            hearts[i] = Instantiate(leftHeart, boarder.transform.position, Quaternion.identity, boarder.transform) as GameObject;
            hearts[i].GetComponent<RectTransform>().localPosition += heartsPosLeft;
            //hearts[i].GetComponent<RectTransform>().position +=  heartsPos;
            //heartsPosRight.x = (-97 + (37.5f * i) / ScreenScale.x);
            heartsPosRight.x = (-97 + (37.5f * i));
            i++;
            hearts[i] = Instantiate(rightHeart, boarder.transform.position, Quaternion.identity, boarder.transform) as GameObject;
            hearts[i].GetComponent<RectTransform>().localPosition += heartsPosRight;
        }

        for (int i = 0; i < maxShields; i++)
        {
            shieldPosLeft = new Vector3(127 - (95 * i), -100);
            shield[i] = Instantiate(shieldPrefab, boarder.transform.position, Quaternion.identity, canvas.transform) as GameObject;
            shield[i].GetComponent<RectTransform>().localPosition += shieldPosLeft;
            shield[i].SetActive(false);
        }
    }

    public void ShowLeftSpell(Sprite powerUp)
    {
        LeftUIIcon.sprite = powerUp;
    }

    public void ShowRightSpell(Sprite powerUp)
    {
        RightUIIcon.sprite = powerUp;
    }

    public void ShowInteractPopUp()
    {
        interactPopup.enabled = true;
    }

    public void ShowEquipPopUp()
    {
        EquipPopup.enabled = true;
    }

    public void HideInteractPopUp()
    {
        interactPopup.enabled = false;
    }

    public void HideEquipPopUp()
    {
        EquipPopup.enabled = false;
    }

}
