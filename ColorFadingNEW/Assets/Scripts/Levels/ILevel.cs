using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILevel
{
    /// <summary>
    /// instead of making animations for every single barrier
    /// </summary>
    /// <param name="_endPos"></param>
    /// <param name="_duration"></param>
    /// <param name="_barrier"></param>
    /// <returns> destroys object after animation finishes</returns>
    IEnumerator EndBarrierAnimation(Vector3 _endPos, float _duration, GameObject _barrier);

    /// <summary>
    /// gets called if Target hit with right color
    /// </summary>
    void TargetHitEvent();

    /// <summary>
    /// To Remove TargetHitEvent
    /// </summary>
    void OnDestroy();
}