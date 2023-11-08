using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject endBarrier;

    [SerializeField] private GameObject waterBarrier;
    [SerializeField] private GameObject bridge1;
    [SerializeField] private GameObject bridge2;

    [Header("Animation input")] [SerializeField]
    private Vector3 posToMoveTo;

    private AudioSource audio;

    private float duration = 10;

    private void Start()
    {
        bridge1.SetActive(false);
        bridge2.SetActive(false);
        waterBarrier.SetActive(false);

        audio = endBarrier.GetComponentInChildren<AudioSource>();

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 1:
                bridge1.SetActive(true);
                break;
            case 2:
                audio.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveTo, duration, endBarrier));
                break;
        }
    }

    public IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier)
    {
        bridge2.SetActive(true);
        waterBarrier.SetActive(true);

        float time = 0;
        Vector3 startPos = _barrier.transform.position;

        while (time < _duration)
        {
            _barrier.transform.position = Vector3.Lerp(startPos, _endPos, time / _duration);
            time += Time.deltaTime;
            yield return null;
        }

        _barrier.transform.position = _endPos;

        yield return new WaitUntil(() => _barrier.transform.position == _endPos);

        Destroy(_barrier);
    }

    public void OnDestroy()
    {
        GameEvents.Instance.onTargetHit -= TargetHitEvent;
    }
}