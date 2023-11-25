using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    [SerializeField] Rigidbody _rigibody;
    [SerializeField] Camera _camera;
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigibody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
        }

        Vector3 point = new Vector3(_camera.pixelWidth / 2 , _camera.pixelHeight / 2);
        Ray ray = _camera.ScreenPointToRay(point);
        Debug.DrawRay(ray.origin, ray.direction * 105f, Color.red);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _rigibody.AddForce(_rigibody.velocity * 45f);
        }
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        _rigibody.AddForce(_camera.transform.forward * vertical * _speed);
        _rigibody.AddForce(_camera.transform.right * horizontal * _speed);

    }
}
