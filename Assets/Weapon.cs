using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
  public Transform firePoint;
  public GameObject bulletPrefab;
  public Animator animator; 

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
            animator.SetFloat("Fire", 1);
        }
    }

    void Shoot ()
    {
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        animator.SetFloat("Fire", -1);
    }
}
