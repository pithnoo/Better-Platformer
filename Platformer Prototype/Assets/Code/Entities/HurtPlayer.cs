using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    [SerializeField] private int damage;
    private DamageDetails damageDetails;
    public bool isProjectile;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            //Debug.Log(other);
            damageDetails.damageAmount = damage;
            other.transform.SendMessage("TakeDamage", damageDetails);

            if(isProjectile){
                Destroy(gameObject);
            }
        }
    }
}
