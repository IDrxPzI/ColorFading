using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject bridge;

    [SerializeField] private GameObject waterBarrier;

    private void Start()
    {
        bridge.SetActive(false);
        waterBarrier.SetActive(false);

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        if (Target.amountTargetsHasBeenHit == 1)
        {
            waterBarrier.SetActive(true);
            bridge.SetActive(true);
        }
    }

    public void OnDestroy()
    {
        GameEvents.Instance.onTargetHit -= TargetHitEvent;
    }
    
    
    //No Use in this Level
    public IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier)
    {
        yield return null;
    }
}