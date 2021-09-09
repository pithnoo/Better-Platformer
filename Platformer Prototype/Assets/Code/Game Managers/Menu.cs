using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject TitleScreen, LevelSelectScreen;
    public LevelLoader levelLoader;


    public void LoadLevel(int levelToLoad){
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
        levelLoader.loadLevel(levelToLoad);
    }

    public void Title(){
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
        LevelSelectScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }

    public void LevelSelect(){
        FindObjectOfType<AudioManager>().Play("ButtonSelect");
        TitleScreen.SetActive(false);
        LevelSelectScreen.SetActive(true);
    }




    public void QuitGame(){
        //audioManager.Play("Pause");
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
