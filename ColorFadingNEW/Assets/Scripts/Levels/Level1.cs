using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Level1 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject endBarrier;
    [SerializeField] private GameObject waterBarrier;
    [SerializeField] private GameObject canvas;

    [Header("Animation input")] [SerializeField]
    private Vector3 posToMoveTo;
    private float duration = 10;

    
    private AudioSource audio;

    private void Start()
    {
        audio = endBarrier.GetComponent<AudioSource>();
        waterBarrier.SetActive(false);
        StartCoroutine(ShowOutput());

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 1:
                waterBarrier.SetActive(true);
                audio.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveTo, duration, endBarrier));
                break;
        }
    }

    /// <summary>
    /// Tutorial output
    /// </summary>
    /// <returns>timer</returns>
    IEnumerator ShowOutput()
    {
        canvas.SetActive(true);

        yield return new WaitForSeconds(4);

        canvas.SetActive(false);
    }

    public IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier)
    {
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