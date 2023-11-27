using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartEvent : MonoBehaviour
{
    [SerializeField] UnityEvent _event;
    [SerializeField] private SceneChanger _gameobject;
    private void Start()
    {
        Invoke(nameof(StartGame),12f);
    }

    public void StartGame()
    {
        _gameobject.ChangeScene();
    }
}
