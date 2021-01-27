using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float sceneChangeDelay; //Scene delay

    private UIManager uIManager;
    private SoundManager soundManager;
    private Animator fade;
    private Scene oldScene;

    //Checks for an instance of LevelManager in current scene
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

       // DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundManager.instance;
        fade = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<Animator>();
        uIManager = UIManager.instance;
    }

  
    private void Update()
    {
        if (fade == null)
        {
            fade = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<Animator>();
        }

        if(SceneManager.GetActiveScene() != oldScene && SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "Options")
        {
            SpawnManager.instance.StartSpawn();
            oldScene = SceneManager.GetActiveScene();
        }
        else
        {
            oldScene = SceneManager.GetActiveScene();
        }

    }

    //Loads level with a delay
    public void LoadLevel(string LevelToLoad)
    {      
        StartCoroutine(ChangeScene(LevelToLoad));
    }

    IEnumerator ChangeScene(string LevelToLoad)
    {
        int levelIndex = SceneManager.GetSceneByName(LevelToLoad).buildIndex;
        if (levelIndex != (int)LevelEnum.MainMenu && levelIndex != (int)LevelEnum.Options)
        {
            uIManager.EnableUI();
        }
        else
        {
            uIManager.DisableUI();
        }
        soundManager.GetComponent<Animator>().SetInteger("Fade", 1);
        fade.SetTrigger("FadeIn");       
         yield return new WaitForSeconds(sceneChangeDelay);
        fade.SetTrigger("FadeOut");
        SceneManager.LoadScene(LevelToLoad);
        soundManager.GetComponent<Animator>().SetInteger("Fade", 0);
       
       // fade = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<Animator>();
    }

    public void NextLevel()
    {
        SpawnManager.instance.currentFloor++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SpawnManager.instance.StartSpawn();
    }

}
