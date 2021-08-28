using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            instructions.SetActive(true);
        }
    }

    // void OnTriggerExit2D(Collider2D other) {
    //     if(other.tag == "Player"){
    //         instructions.SetActive(false);
    //     }
    // }
}
