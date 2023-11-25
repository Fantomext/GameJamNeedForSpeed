using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _linerRenderer;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _start;

    private void Update()
    {
        DrawLine();
    }


    public void DrawLine()
    {
        _linerRenderer.SetPosition(0, _start.position);
        _linerRenderer.SetPosition(1, _target.position);
    }
}
