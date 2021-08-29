using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime;
    public Animator transition;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void loadLevel(int sceneNumber) => StartCoroutine(LoadLevel(sceneNumber));

    public void loadNextLevel() => StartCoroutine(LoadAndSave(SceneManager.GetActiveScene().buildIndex + 1));

    private IEnumerator LoadLevel(int levelIndex){
        //Cursor.visible = false;
        audioManager.stopPlaying("MenuTheme");
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);
        audioManager.Play("LevelTheme");
    }

    private IEnumerator LoadAndSave(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
