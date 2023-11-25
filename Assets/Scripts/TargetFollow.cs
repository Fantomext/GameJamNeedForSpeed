using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void Update()
    {
        Vector3 target = _target.position;
        target.y = 0;
        transform.position = target;
    }
}
