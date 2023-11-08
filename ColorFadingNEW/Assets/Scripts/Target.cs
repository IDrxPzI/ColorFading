using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    [Header("TargetsToHit")]

    public static int amountTargetsHasBeenHit;

    private bool hitOnce;

    /// <summary>
    /// when hit with correct color change color of child objects
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        //set child colors of the Target to parent color
        if (other.gameObject.CompareTag("Bullet"))
        {
            Color myColor = GetComponentInParent<Renderer>().material.color;
            Color otherColor = other.gameObject.GetComponent<Renderer>().material.color;

            //compare color of bullet to color of target
            if (myColor.Equals(otherColor))
            {
                SoundManager.Instance.PlaySFX("Target_Hit_Right_Color");

                foreach (Transform child in transform)
                {
                    child.GetComponent<Renderer>().material.color = myColor;
                }

                if (!hitOnce)
                {
                    amountTargetsHasBeenHit++;

                    GameEvents.Instance.TargetHitEvent();
                    hitOnce = true;
                }
            }
            else
            {
                SoundManager.Instance.PlaySFX("Target_Hit_Wrong_Color");
            }
        }
    }
}