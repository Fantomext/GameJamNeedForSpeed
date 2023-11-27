using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audio;
  

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audio[0].Stop();
            _audio[1].Play();
            Invoke(nameof(LoadFirstScene), 3f);
        }
    }

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
