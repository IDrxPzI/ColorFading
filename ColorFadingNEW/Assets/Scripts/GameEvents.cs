using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    public event Action onTargetHit;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// call event when target got hit
    /// </summary>
    public void TargetHitEvent()
    {
        if (onTargetHit != null)
        {
            onTargetHit();
        }
    }
}