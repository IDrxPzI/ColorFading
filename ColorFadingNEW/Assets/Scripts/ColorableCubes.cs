using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorableCubes : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //to color the cube with the color of the bullet
        var bulletColor = other.gameObject.GetComponent<Renderer>().material.color;

        if (other.gameObject.CompareTag("Bullet"))
        {
            gameObject.GetComponent<Renderer>().material.color = bulletColor;
        }
    }
}