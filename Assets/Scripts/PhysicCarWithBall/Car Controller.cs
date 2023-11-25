using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;

    [SerializeField] private float _maxSpeed  = 50f;
    [SerializeField] private float _forwardAccel = 8f;
    [SerializeField] private float _reverseAccel = 4f;
    [SerializeField] private float _turnStrength = 180f;

    private float _horizontal;
    private float _vertical;
    private void Start()
    {
        _rigibody.transform.parent = null;
    }
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        transform.position = _rigibody.transform.position;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,_horizontal * _turnStrength * Time.deltaTime,0f));

    }
    private void FixedUpdate()
    {
        _rigibody.AddForce(transform.forward * _vertical * _forwardAccel * 100);
    }
}
