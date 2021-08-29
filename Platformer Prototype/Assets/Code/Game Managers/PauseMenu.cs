using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public LevelManager levelManager;
    public LevelLoader levelLoader;
    public AudioManager audioManager;

    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
        //audioManager = FindObjectOfType<AudioManager>();
    }


    public void PauseUpdate(){
        if(GameIsPaused && !levelManager.isGameOver){
            Resume();
        }
        else{
            Pause();
        }
    }

    public void Resume(){
        // FindObjectOfType<AudioManager>().Play("Pause");
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause(){
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu(){
        AudioListener.pause = true;
        AudioListener.volume = 1f;
        
        Time.timeScale = 1;
    }

    public void QuitGame(){
        //audioManager.Play("Pause");
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}