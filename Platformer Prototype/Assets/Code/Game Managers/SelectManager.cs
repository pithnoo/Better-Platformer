using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Image[] gems;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("Collectable1")){
            for (int i = 0; i < gems.Length; i++)
            {
                PlayerPrefs.SetInt("Collectable" + i.ToString(), 0);
            }
        }

        for(int i = 0; i < gems.Length; i++){
            if(PlayerPrefs.GetInt("Collectable" + i.ToString()) == 1){
                gems[i].enabled = true;
            }
            else{
                gems[i].enabled = false;
            }
        }

    }
}
