using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private UnityEvent _eventOpenWindow;
    [SerializeField] private UnityEvent _eventCloseWindow;

    [SerializeField] private MonoBehaviour[] _comonentsToDisable;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menuWindow.activeSelf == false)
            {
                OpenMenuWindow();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_menuWindow.activeSelf == true)
            {
                CloseMenuWindow();
            }
        }
    }
    public void OpenMenuWindow()
    {
        _menuWindow.SetActive(true);
        _gameMenu.SetActive(false);
        _eventOpenWindow.Invoke();
        for (int i = 0; i < _comonentsToDisable.Length; i++)
        {
            _comonentsToDisable[i].enabled = false;
        }

        Time.timeScale = 0.01f;
        Cursor.visible = true;
    }

    public void CloseMenuWindow()
    {
        _menuWindow.SetActive(false);
        _gameMenu.SetActive(true);
        _eventOpenWindow.Invoke();
        for (int i = 0; i < _comonentsToDisable.Length; i++)
        {
            _comonentsToDisable[i].enabled = true;
        }
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
