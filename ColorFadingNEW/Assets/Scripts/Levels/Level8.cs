using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject Teleporter;

    private void Start()
    {
        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 2:
                //did we got ripped off our colors?
                break;
            case 4:
                Teleporter.SetActive(true);
                break;
        }
    }

    //No Use in this Level
    public IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier)
    {
        yield return null;
    }


    public void OnDestroy()
    {
        GameEvents.Instance.onTargetHit -= TargetHitEvent;
    }
}