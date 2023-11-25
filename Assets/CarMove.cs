using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] Rigidbody _rigibody;
    [SerializeField] private float _speed;
    [SerializeField] private float _torqueSpeed;


    void FixedUpdate()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        _rigibody.AddForce(transform.forward * vertical * _speed);
        _rigibody.AddTorque(transform.up * horizontal * _torqueSpeed);
        
    }
}
