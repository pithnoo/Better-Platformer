using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeTween : MonoBehaviour
{
    public float delay;
    public float duration;
    public float inOpacity;
    public float outOpacity;
    public LeanTweenType tweenType;
    public Image imageFade;
    public GameObject test;
    private LevelLoader levelLoader;

    
    void Start(){
        levelLoader = FindObjectOfType<LevelLoader>();
        LeanTween.alpha(imageFade.rectTransform, inOpacity, 1f).setDelay(delay).setEase(tweenType).setOnComplete(fadeOut);
    }

    void fadeOut(){
        
        LeanTween.alpha(imageFade.rectTransform, outOpacity, 1f).setDelay(delay).setEase(tweenType).setOnComplete(menu);
    }
    void menu() => levelLoader.loadMenu();

    
}
