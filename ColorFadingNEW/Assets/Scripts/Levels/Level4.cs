using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Level4 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject endBarrier;

    [SerializeField] private GameObject waterBarrier;
    [SerializeField] private GameObject stairs;

    [Header("Tutorial Canvas")] [SerializeField]
    private GameObject canvas;

    [SerializeField] private TMP_Text text;

    [Header("Animation input")] [SerializeField]
    private Vector3 posToMoveTo;
    private float duration = 10;
    

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        stairs.SetActive(false);
        waterBarrier.SetActive(false);

        StartCoroutine(ShowOutput());

        audio = endBarrier.GetComponentInChildren<AudioSource>();

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 1:
                stairs.SetActive(true);
                break;
            case 2:
                audio.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveTo, duration, endBarrier));
                break;
        }
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


    //Tutorial output
    IEnumerator ShowOutput()
    {
        canvas.SetActive(true);

        yield return new WaitForSeconds(4);

        text.SetText("Pick up a Color");

        yield return new WaitForSeconds(4);

        text.SetText("while having a Color on the muzzle ");

        yield return new WaitForSeconds(4);

        canvas.SetActive(false);
    }

    public void OnDestroy()
    {
        GameEvents.Instance.onTargetHit -= TargetHitEvent;
    }
}