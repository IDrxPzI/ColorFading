using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwayMainMenu : MonoBehaviour
{
    [Header("CameraMovement Settings")] [SerializeField]
    Vector3 startAngle;

    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float rotationOffset = 30f;

    float finalAngle;

    // Update is called once per frame
    void Update()
    {
        //move camera left/right
        finalAngle = startAngle.y + Mathf.Sin(Time.time * rotationSpeed) * rotationOffset;
        transform.eulerAngles = new Vector3(startAngle.x, finalAngle, startAngle.z);
    }
}