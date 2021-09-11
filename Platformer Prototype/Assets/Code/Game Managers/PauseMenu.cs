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
    public bool canPause = true;


    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }


    public void PauseUpdate(){
        if(canPause){
            if (GameIsPaused && !levelManager.isGameOver)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume(){
        Cursor.visible = false;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause(){
        Cursor.visible = true;
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu(){
        canPause = false;
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
        levelLoader.loadMenu();
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    public void QuitGame(){
        //audioManager.Play("Pause");
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}