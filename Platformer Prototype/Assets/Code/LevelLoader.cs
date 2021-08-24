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

    public void loadAndSave(int sceneNumber) => StartCoroutine(LoadAndSave(sceneNumber));

    private IEnumerator LoadLevel(int levelIndex){
        //Cursor.visible = false;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);
    }

    private IEnumerator LoadAndSave(int levelIndex){
        yield return new WaitForSeconds(transitionTime);
    }
}
