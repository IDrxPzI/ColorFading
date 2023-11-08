using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject endBarrier;

    [SerializeField] private GameObject[] waterBarriers;

    [Header("Animation input")] [SerializeField]
    private Vector3 posToMoveTo;

    private float duration = 10;


    private AudioSource audio;

    private void Start()
    {
        for (int i = 0; i < waterBarriers.Length; i++)
        {
            waterBarriers[i].SetActive(false);
        }

        audio = endBarrier.GetComponent<AudioSource>();

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 6:
                audio.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveTo, duration, endBarrier));
                break;
        }
    }

    public IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier)
    {
        for (int i = 0; i < waterBarriers.Length; i++)
        {
            waterBarriers[i].SetActive(true);
        }

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