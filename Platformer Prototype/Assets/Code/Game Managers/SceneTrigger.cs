using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource source1, source2;
    [SerializeField] private GameObject particle1, particle2;
    private CinemachineSwitcher cinemachineSwitcher;
    public Transform objectPosition;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineSwitcher = FindObjectOfType<CinemachineSwitcher>();
        //cinemachineSwitcher.SwitchState();
    }

    public void sceneShake1() => source1.GenerateImpulse();
    public void sceneShake2() => source2.GenerateImpulse();
    public void sceneParticle(){
        Instantiate(particle1, objectPosition.transform.position, objectPosition.transform.rotation);
        Instantiate(particle2, objectPosition.transform.position, objectPosition.transform.rotation);
    }
    
    public void switchBack() => cinemachineSwitcher.SwitchState();
        
}
