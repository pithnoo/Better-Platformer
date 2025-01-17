using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private float bossWaitTime;
    [SerializeField] private GameObject bossGate, bossGate2, gateParticle;
    [SerializeField] private CinemachineSwitcher cs;
    private bool hasSwitched = false;
    void Start() {
        cs = FindObjectOfType<CinemachineSwitcher>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            FindObjectOfType<Player>().canMove = false;
            //change current camera
            if(!hasSwitched){
                FindObjectOfType<AudioManager>().stopPlaying("LevelTheme");
                FindObjectOfType<AudioManager>().Play("BossGate");
                cs.SwitchState();
                hasSwitched = true;
            }

            bossGate.SetActive(true);
            Instantiate(gateParticle, bossGate.transform.position, bossGate.transform.rotation);

            bossGate2.SetActive(true);
            Instantiate(gateParticle, bossGate2.transform.position, bossGate2.transform.rotation);
            
            StartCoroutine("bossTimer");
        }
    }

    private IEnumerator bossTimer(){
        yield return new WaitForSeconds(bossWaitTime);
        boss.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
