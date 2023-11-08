using System;
using System.Collections;
using UnityEngine;

public class Level6 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject middleBarrier;

    [SerializeField] private GameObject endBarrier;
    [SerializeField] private GameObject WaterCollider;
    [SerializeField] private GameObject endWaterBarrier;

    [SerializeField] private ParticleSystem WaterBarrier;

    [Header("Animation input")] [SerializeField]
    private Vector3 posToMoveTo;

    [SerializeField] private Vector3 posToMoveToMiddle;

    private AudioSource audioMiddle;
    private AudioSource audioEnd;

    private float duration = 10;

    private void Start()
    {
        endWaterBarrier.SetActive(false);

        audioMiddle = middleBarrier.GetComponentInChildren<AudioSource>();
        audioEnd = endBarrier.GetComponentInChildren<AudioSource>();

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 1:
                audioMiddle.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveToMiddle, duration, middleBarrier));
                break;
            case 2:
                WaterBarrier.Stop();
                WaterCollider.SetActive(false);
                break;
            case 3:
                audioEnd.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveTo, duration, endBarrier));
                break;
        }
    }

    public IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier)
    {
        endWaterBarrier.SetActive(true);

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