using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : MonoBehaviour
{

    [SerializeField] private GameObject _past;
    [SerializeField] private GameObject _future;
    [SerializeField] TimeState _timeState;

    private void Start()
    {
        
    }

    [ContextMenu("Future")]
    public void ChangeTimeFuture()
    {
        _future.SetActive(true);
        _past.SetActive(false);
    }
    [ContextMenu("Past")]
    public void ChangeTimePast()
    {
        _past.SetActive(true);
        _future.SetActive(false);
    }

    public void SetStateFuture()
    {
        _timeState = TimeState.future;
    }
    public void SetStatePast()
    {
        _timeState = TimeState.past;
    }
}
