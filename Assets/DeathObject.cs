using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    [SerializeField] private float _speedToDie = 5f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.TryGetComponent<OnEnterHit>(out var hitCar))
        {
            hitCar.DieCheck(_speedToDie);
        }
    }
}
