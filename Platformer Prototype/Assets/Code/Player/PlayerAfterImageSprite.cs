using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer SR;
    private SpriteRenderer playerSR;
    private Color colour;
    [SerializeField] private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    [SerializeField] private float alphaSet = 0.8f;
    [SerializeField] private float alphaMultiplier = 0.05f;

    private void OnEnable(){
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActivated = Time.time;
    }

    private void Update(){
        alpha *= alphaMultiplier;
        colour = new Color(1f,1f,1f, alpha);
        SR.color = colour;

        if(Time.time >= (timeActivated + activeTime)){
            //add to pool
            PlayerAfterImagePool.instance.AddToPool(gameObject);
        }
    }
}
