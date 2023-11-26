using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TimeState
{
    past,
    future
}

public class TimeManager : MonoBehaviour
{
    [SerializeField] private List<TimeItem> timeItems = new List<TimeItem>();
    [SerializeField] TimeState _timeState;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_timeState == TimeState.past)
            {
                _timeState = TimeState.future;
            }
            else if (_timeState == TimeState.future)
            {
                _timeState = TimeState.past;
            }
           
            StartChangeTime();
        }
    }

    public void StartChangeTime()
    {
        StartCoroutine(ChangeTime());
    }

    IEnumerator ChangeTime()
    {
        if (_timeState == TimeState.past)
        {
            foreach (var item in timeItems)
            {
                item.ChangeTimePast();
                yield return null;
            }
        }
        if (_timeState == TimeState.future)
        {
            foreach (var item in timeItems)
            {
                item.ChangeTimeFuture();
                yield return null;
            }
        }
    }
}
