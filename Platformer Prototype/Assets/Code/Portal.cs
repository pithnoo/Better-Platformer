using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Player player;
    public LayerMask whatIsPlayer;
    public float detectionRadius;
    public bool isWithPlayer;
    public GameObject portalParticle;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        isWithPlayer = Physics2D.OverlapCircle(transform.position, detectionRadius, whatIsPlayer);

        if(isWithPlayer && player.isDashing){
            player.gameObject.SetActive(false);
            Instantiate(portalParticle, transform.position, transform.rotation);
        }
    }
}
