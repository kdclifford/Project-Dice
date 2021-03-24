using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("UI Refereneces")]
    private Image LeftUIIcon;
    [SerializeField]
    private Image RightUIIcon;
    [SerializeField]
    private GameObject interactPopup;
    [SerializeField]
    private GameObject EquipPopup;
    [SerializeField]
    private GameObject shieldPrefab;
    //[SerializeField]
    //private GameObject boarder;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private GameObject HeartUI;
    [SerializeField]
    private GameObject ManaUI;
    [SerializeField]
    private Text ManaText;

    private Health playerHealth;
   // public GameObject[] hearts;
    public GameObject[] shield;
   // private int maxHearts;
    private int maxShields;


    private Scene currentScene;

    public static UIManager instance;

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
        currentScene = SceneManager.GetActiveScene();

    }


    public void RemoveHeart()
    {
        if (playerHealth.GetShield() > 0)
        {
            shield[playerHealth.GetShield()- 1].SetActive(false);
            return;
        }

        if (playerHealth.GetHealth() > 0)
        {

        }
    }

    public void AddUIShield()
    {
        shield[playerHealth.GetShield()].SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        maxShields = (int)playerHealth.maxShields;
        shield = new GameObject[maxShields];
        Vector3 heartsPosLeft = new Vector3(-1, 0);
        Vector3 heartsPosRight = new Vector3(-1, 0);
        Vector3 shieldPosLeft = new Vector3(-1, 0);

        //for (int i = 0; i < maxShields; i++)
        //{
        //    shieldPosLeft = new Vector3(-230 - (100 * i), -70);
        //    shield[i] = Instantiate(shieldPrefab, boarder.transform.position, Quaternion.identity, canvas.transform) as GameObject;
        //    shield[i].GetComponent<RectTransform>().localPosition += shieldPosLeft;
        //    shield[i].SetActive(false);
        //}
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() != currentScene)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
            maxShields = (int)playerHealth.maxShields;
            HideEquipPopUp(); HideInteractPopUp();
            currentScene = SceneManager.GetActiveScene();
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
        interactPopup.SetActive(true);
    }

    public void ShowEquipPopUp()
    {
        EquipPopup.SetActive(true);
    }

    public void HideInteractPopUp()
    {
        interactPopup.SetActive(false);
    }

    public void HideEquipPopUp()
    {
        EquipPopup.SetActive(false);
    }

    public void EnableUI()
    {
        gameUI.SetActive(true);
    }

    public void DisableUI()
    {
        gameUI.SetActive(false);
    }

    public void EnableBarUI()
    {
        HeartUI.SetActive(true);
        ManaUI.SetActive(true);
    }
    public void DisableBarUI()
    {
        HeartUI.SetActive(false);
        ManaUI.SetActive(false);
    }

    public void updateMana(int manaValue)
    {
        ManaText.text = manaValue.ToString();
    }
}
