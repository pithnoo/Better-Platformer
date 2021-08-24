using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime;
    public Animator transition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void loadLevel(int sceneNumber) => StartCoroutine(LoadLevel(sceneNumber));

    public void loadNextLevel() => StartCoroutine(LoadAndSave(SceneManager.GetActiveScene().buildIndex + 1));

    private IEnumerator LoadLevel(int levelIndex){
        //Cursor.visible = false;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    private IEnumerator LoadAndSave(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
