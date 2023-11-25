using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RopeState
{
    Disabled,
    Fly,
    Active
}

public class RopeGun : MonoBehaviour
{
    [SerializeField] private Hook _hook;
    [SerializeField] private Transform _spawn;
    [SerializeField] private float _speed;
    [SerializeField] private SpringJoint _springJoint;
    [SerializeField] private Transform _bulletSpawn;
    private float _length;
    public RopeState _state;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroySprine();
        }
        if (_state == RopeState.Fly)
        {
            float dis = Vector3.Distance(_bulletSpawn.position, _hook.transform.position);
            Debug.Log(dis);
            if (dis > 50)
            {
                _hook.gameObject.SetActive(false);
                _state = RopeState.Disabled;
            }
        }
    }

    public void Shoot()
    {
        if (_springJoint)
        {
            Destroy(_springJoint);
        }
        _hook.gameObject.SetActive(true);
        _hook.StopFix();
        _hook.transform.position = _spawn.position;
        _hook.transform.rotation = _spawn.rotation;
        _hook.SetSpeed(_spawn.forward, _speed);
        _state = RopeState.Fly;
    }

    public void CreateSpring()
    {
        if (_springJoint == null)
        {
            _springJoint = gameObject.AddComponent<SpringJoint>();
            _springJoint.connectedBody = _hook._rigibody;
            _springJoint.anchor = Vector3.zero;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = Vector3.zero;
            _springJoint.spring = 100f;
            _springJoint.damper = 5f;


            _length = Vector3.Distance(_bulletSpawn.position, _hook.transform.position);
            _springJoint.maxDistance = _length;

        }
        _state = RopeState.Active;
    }

    public void DestroySprine()
    {
        if (_springJoint)
        {
            Destroy(_springJoint);
            _state = RopeState.Disabled;
            _hook.gameObject.SetActive(false);
        }
    }
}
