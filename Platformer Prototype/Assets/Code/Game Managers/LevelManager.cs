using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player player;
    public Soul soul;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        soul = FindObjectOfType<Soul>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerCheck() => player = FindObjectOfType<Player>();
    public void ResetSoulCheck() => soul = FindObjectOfType<Soul>();
}
