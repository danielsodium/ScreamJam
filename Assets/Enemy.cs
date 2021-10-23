using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 40;
    public GameObject deathEffect;
    public void TakeDamage (int damage){
        health -= damage;
        if (health <=0){
            Die();
        }
    }

    void Die (){
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
   void OnTriggerEnter2D (Collider2D hitInfo){
        Character_Health character_Health = hitInfo.GetComponent<Character_Health>();
        if(character_Health != null){
            character_Health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
