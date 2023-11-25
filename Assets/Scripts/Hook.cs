using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private FixedJoint _fixedJoin;
    [SerializeField] public Rigidbody _rigibody;

    [SerializeField] private Collider _playerCollider;
    [SerializeField] private Collider _collider;

    [SerializeField] private RopeGun _ropeGun;

    private void Start()
    {
        Physics.IgnoreCollision(_collider, _playerCollider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_fixedJoin == null)
        {
            _fixedJoin = gameObject.AddComponent<FixedJoint>();
            if (collision.rigidbody)
            {
                _fixedJoin.connectedBody = collision.rigidbody;
            }
            _ropeGun.CreateSpring();
        }
    }

    public void StopFix()
    {
        if (_fixedJoin)
        {
            Destroy(_fixedJoin);
        }
    }
    public void SetSpeed(Vector3 dir, float speed)
    {
        _rigibody.velocity = dir * speed;
    }
}
