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
    [SerializeField] private GameObject _Image;
    [SerializeField] private TerrainChanger _terrainChanger;

    private void Update()
    {
        if (_car.ReturnSpeed() > 61)
        {
            _Image.SetActive(true);
        }
        else
        {
            _Image.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _car.ReturnSpeed() > 61)
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
            _changeTimeEffect.SetActive(true);
            _terrainChanger.MakeOld();
            foreach (var item in timeItems)
            {
                item.ChangeTimePast();
            }
        }
        else if (_timeState == TimeState.future)
        {
            _changeTimeEffect.SetActive(true);
            _terrainChanger.MakeModern();
            foreach (var item in timeItems)
            {
                item.ChangeTimeFuture();
            }
        }
        yield return new WaitForSeconds(0.5f);
        _changeTimeEffect.SetActive(false);

    }
}
