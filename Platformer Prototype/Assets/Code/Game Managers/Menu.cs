using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject TitleScreen, LevelSelectScreen;
    public LevelLoader levelLoader;


    public void LoadLevel(int levelToLoad){
        levelLoader.loadLevel(levelToLoad);
    }

    public void Title(){
        LevelSelectScreen.SetActive(false);
        TitleScreen.SetActive(true);
    }

    public void LevelSelect(){
        TitleScreen.SetActive(false);
        LevelSelectScreen.SetActive(true);
    }




    public void QuitGame(){
        //audioManager.Play("Pause");
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
