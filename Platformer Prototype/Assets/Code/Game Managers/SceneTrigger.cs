using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource source1, source2;
    [SerializeField] private GameObject particle1, particle2, particle3;
    private CinemachineSwitcher cinemachineSwitcher;
    public Transform objectPosition;
    public LevelLoader levelLoader;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        //audioManager = FindObjectOfType<AudioManager>();
    }

    public void startTheme() {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("IntroTheme");
    }
    public void heartBeat(){
        audioManager.Play("Heartbeat");
        Instantiate(particle1, objectPosition.transform.position, objectPosition.transform.rotation);
    }
        
    public void sceneShake1(){
        audioManager.Play("BossStart");
        source1.GenerateImpulse();
    }

    public void cutHeart() => audioManager.stopPlaying("Heartbeat");
    public void sceneShake2(){
        audioManager.Play("BossEnd");
        source2.GenerateImpulse();
    }
    public void sceneParticle(){
        Instantiate(particle2, objectPosition.transform.position, objectPosition.transform.rotation);
        Instantiate(particle3, objectPosition.transform.position, objectPosition.transform.rotation);
    }
    
    public void finishStart(){
        levelLoader.loadLevel(1);
    }
        
}
