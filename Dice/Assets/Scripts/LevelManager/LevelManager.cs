using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float sceneChangeDelay; //Scene delay

    private SoundManager soundManager;
    private Animator fade;

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

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        fade = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<Animator>();
    }

  
    private void Update()
    {
        if (fade == null)
        {
            fade = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<Animator>();
        }
    }

    //Loads level with a delay
    public void LoadLevel(int levelIndex)
    {      
        StartCoroutine(ChangeScene(levelIndex));
    }

    IEnumerator ChangeScene( int levelIndex)
    {
        soundManager.GetComponent<Animator>().SetInteger("Fade", 1);
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(sceneChangeDelay);
        SceneManager.LoadScene(levelIndex);
        soundManager.GetComponent<Animator>().SetInteger("Fade", 0);
        fade = GameObject.FindGameObjectWithTag("SceneFade").GetComponent<Animator>();
    }
}
