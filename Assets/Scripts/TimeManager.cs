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
    [SerializeField] private GameObject _changeTimeEffect;
    [SerializeField] private AudioSource _timeTravelerSound;
    [SerializeField] private CarController _car;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _car.ReturnSpeed() > 61)
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
        _timeTravelerSound.Play();
        if (_timeState == TimeState.past)
        {
            foreach (var item in timeItems)
            {
                _changeTimeEffect.SetActive(true);
                item.ChangeTimePast();
                yield return new WaitForSeconds(0.1f);
                _changeTimeEffect.SetActive(false);
            }
        }
        else if (_timeState == TimeState.future)
        {
            foreach (var item in timeItems)
            {
                _changeTimeEffect.SetActive(true);
                item.ChangeTimeFuture();
                yield return new WaitForSeconds(0.1f);
                _changeTimeEffect.SetActive(false);
            }
        }
    }
}
