using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] lvlButtons;

    // Start is called before the first frame update
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);

        for(int i = 0; i < lvlButtons.Length; i++){
            if (i + 1 > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
