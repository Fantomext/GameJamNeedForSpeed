using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _music;

    private void Start()
    {
        
    }

    public void SetMusicEnabled()
    {
        AudioListener.volume = 1;
    }
    public void SetMusicDisabled()
    {
        AudioListener.volume = 0;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
