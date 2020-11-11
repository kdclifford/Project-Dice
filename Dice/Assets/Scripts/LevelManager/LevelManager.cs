using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private SoundManager soundManager;
    public Animator fade;
    public float sceneChangeDelay;
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
    }

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
    }


}
