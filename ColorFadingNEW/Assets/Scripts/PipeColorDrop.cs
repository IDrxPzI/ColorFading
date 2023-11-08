using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeColorDrop : MonoBehaviour
{
    [SerializeField] private ParticleSystem colorDrop;

    void Start()
    {
        var colorDropEmission = colorDrop.emission;
        colorDropEmission.rateOverTime = 0;
    }

    /// <summary>
    /// start particlesystem to color the bucket
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        var colorDropEmission = colorDrop.emission;

        if (other.gameObject.CompareTag("Bullet"))
        {
            colorDropEmission.rateOverTime = 0.6f;
        }
    }
}