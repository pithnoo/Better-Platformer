using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// public static class Collectable{
//     public static bool[] itemCollected = new bool[5];
//     public static void hasCollected(int levelCollectedFrom){
//         itemCollected[levelCollectedFrom] = true;
//     }
// }
public class LevelLoader : MonoBehaviour
{
    public float transitionTime;
    public Animator transition;
    private AudioManager audioManager;
    [SerializeField] private int nextSceneLoad;

    // Start is called before the first frame update
    void Start() {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void loadLevel(int sceneNumber) => StartCoroutine(LoadLevel(sceneNumber));
    public void loadMenu() => StartCoroutine("LoadMenu");
    public void finishGame() => StartCoroutine("FinishGame");
    public void startScene() => StartCoroutine("StartScene");

    public void loadNextLevel() => StartCoroutine(LoadAndSave(SceneManager.GetActiveScene().buildIndex + 1));

    private IEnumerator LoadLevel(int levelIndex){
        Cursor.visible = false;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.stopPlaying(audioManager.currentTheme);

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(levelIndex);

        audioManager.Play("LevelTheme");
        audioManager.currentTheme = "LevelTheme";
    }

    private IEnumerator LoadMenu(){
        audioManager = FindObjectOfType<AudioManager>();

        //Debug.Log(audioManager.currentTheme);
        audioManager.stopPlaying(audioManager.currentTheme);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(0);

        audioManager.Play("MenuTheme");
        audioManager.currentTheme = "MenuTheme";
    }

    private IEnumerator StartScene(){
        audioManager.stopPlaying(audioManager.currentTheme);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(6);
    }

    private IEnumerator FinishGame(){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync(6);
    }

    private IEnumerator LoadAndSave(int levelIndex){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        if(nextSceneLoad > PlayerPrefs.GetInt("levelAt")){
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }

        SceneManager.LoadSceneAsync(levelIndex);
    }
}
