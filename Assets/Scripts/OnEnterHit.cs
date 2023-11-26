using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterHit : MonoBehaviour
{
    [SerializeField] private CarController _carController;
    [SerializeField] ParticleSystem _particleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contacts[0].normal.ToString());
        if (collision.contacts[0].normal.x >= 0.4f || collision.contacts[0].normal.x <= -0.4f)
        {
            _particleSystem.Play();
            _particleSystem.transform.position = collision.contacts[0].point;
        }
        
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

    public void ChangeSpeed(int value)
    {
        _carController.BoosterStart(value);
    }
}
