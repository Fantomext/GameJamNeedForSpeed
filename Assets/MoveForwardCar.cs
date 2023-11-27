using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardCar : MonoBehaviour
{
    [SerializeField] Rigidbody _rigibody;

    private void FixedUpdate()
    {
        _rigibody.AddForce(transform.forward * 6f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
