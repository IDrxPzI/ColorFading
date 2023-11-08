using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level5 : MonoBehaviour, ILevel
{
    [Header("Gameobjects")] [SerializeField]
    private GameObject stairs1;

    [SerializeField] private GameObject stairs2;
    [SerializeField] private GameObject endBarrier;
    [SerializeField] private GameObject waterBarrier;

    [Header("Animation input")] [SerializeField]
    private Vector3 posToMoveTo;
    private float duration = 10;
    
    private AudioSource audio;


    // Start is called before the first frame update
    void Start()
    {
        stairs1.SetActive(false);
        stairs2.SetActive(false);
        waterBarrier.SetActive(false);
        audio = endBarrier.GetComponentInChildren<AudioSource>();

        GameEvents.Instance.onTargetHit += TargetHitEvent;
    }

    public void TargetHitEvent()
    {
        switch (Target.amountTargetsHasBeenHit)
        {
            case 1:
                stairs1.SetActive(true);
                break;
            case 2:
                stairs2.SetActive(true);
                break;
            case 3:
                audio.Play();
                StartCoroutine(EndBarrierAnimation(posToMoveTo, duration, endBarrier));
                break;
            case 4: //easteregg
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


    public void OnDestroy()
    {
        GameEvents.Instance.onTargetHit -= TargetHitEvent;
    }
}