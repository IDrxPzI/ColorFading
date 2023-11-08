using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBarrier : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    private void OnTriggerEnter(Collider other)
    {
        //remove colors from player
        if (other.CompareTag("Player"))
        {
            weapon.ResetColor();
        }

        //prevent bullets to go through 
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}