using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{

    [SerializeField] private Material _futureMat;
    [SerializeField] private Material _pastMat;
    private void Start()
    {
        _futureMat = RenderSettings.skybox;
    }

    public void FutureSky()
    {
        RenderSettings.skybox = _futureMat;
    }

    public void PastSky()
    {
        RenderSettings.skybox = _pastMat;
    }
}
