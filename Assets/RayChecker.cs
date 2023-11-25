using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayChecker : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private UnityEvent _eventClose;
    [SerializeField] private UnityEvent _eventExit;


    private void Update()
    {
        Ray ray = new Ray(transform.position, direction);
        Debug.DrawRay(ray.origin, ray.direction * 0.2f);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 0.2f))
        {
            _eventClose.Invoke();
        }
        else
        {
            _eventExit.Invoke();
        }
    }
}
