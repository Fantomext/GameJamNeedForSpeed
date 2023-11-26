using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterHit : MonoBehaviour
{
    [SerializeField] private CarController _carController;
    [SerializeField] ParticleSystem _particleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        _particleSystem.transform.position = collision.contacts[0].point;
        _particleSystem.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        _particleSystem.Stop();
    }

    public void DieCheck(float target)
    {
        if(_carController.ReturnSpeed() > target)
        {
            _carController.DieFromSpeed();
        }
    }
}
