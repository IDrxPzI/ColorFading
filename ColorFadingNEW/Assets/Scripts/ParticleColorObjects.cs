using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleColorObjects : MonoBehaviour
{
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// set the color of the bucket to the color of the particle
    /// </summary>
    /// <param name="other"></param>
    private void OnParticleCollision(GameObject other)
    {
        //set the color of the bucket the same as the particle
        if (other.CompareTag("Bucket"))
        {
            other.layer = LayerMask.NameToLayer("Color");
            other.GetComponent<Renderer>().material.color = Color.blue;
            
            audio.PlayOneShot(audio.clip);
        }
    }
}