using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private float bossWaitTime;
    public CinemachineVirtualCamera currentCamera;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //change current camera
            StartCoroutine("bossTimer");
        }
    }

    private IEnumerator bossTimer(){
        yield return new WaitForSeconds(bossWaitTime);
        boss.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
