using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts;
    [SerializeField] private Sprite heartEmpty, heartHalf, heartFull;
    
    public void UpdateHealth(int currentHealth){
        for(int i = 0; i < hearts.Length; i++){

            int remainder = currentHealth - (i * 2);
            //Debug.Log(remainder);
            switch(remainder){
                case 0:
                hearts[i].sprite = heartEmpty;
                break;
                case 1:
                hearts[i].sprite = heartHalf;
                break;
                case 2:
                hearts[i].sprite = heartFull;
                break;
            }
        }
    }
}
