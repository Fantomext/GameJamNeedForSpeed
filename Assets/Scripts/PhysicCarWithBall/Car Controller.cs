using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private GameObject _visual;

    [SerializeField] private float _maxSpeed  = 50f;
    [SerializeField] private float _forwardAccel = 8f;
    [SerializeField] private float _reverseAccel = 4f;
    [SerializeField] private float _turnStrength = 180f;
    [SerializeField] private float _gravityForce = 10f;
    [SerializeField] private float _dragOnGround = 3f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _accelerationSpeed = 10f;
    [SerializeField] CinemachineVirtualCamera _cineMachine;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private bool _grounded;

    [SerializeField] private float groundRayLength = 0.5f;
    [SerializeField] private Transform _groundRayPoint;
    [SerializeField] private float _velocityValueChange;
    [SerializeField] private ParticleSystem[] _particles;
    private void Start()
    {
        _rigibody.transform.parent = null;
    }
    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        transform.position = _rigibody.transform.position;

        if (_grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f,_horizontal * _turnStrength * Time.deltaTime * _vertical,0f));
        }

        RaycastHit hit;
        _grounded = false;

        if (Physics.Raycast(_groundRayPoint.position, -transform.up, out hit, groundRayLength))
        {
            _grounded = true;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation, Time.deltaTime * 3f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigibody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _forwardAccel += _accelerationSpeed;
            SmoothChangeValue(0.3f);
            _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
            _particles[0].Play();
            _particles[1].Play();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _forwardAccel -= _accelerationSpeed;
            SmoothChangeValue(0f);
            _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
            _particles[0].Stop();
            _particles[1].Stop();
        }

    }
    private void FixedUpdate()
    {
        
        if (_grounded)
        {
            _rigibody.AddForce(transform.forward * _vertical * _forwardAccel * 100);
            _rigibody.drag = _dragOnGround;
        }
        else
        {
            _rigibody.drag = 0.1f;
            _rigibody.AddForce(Vector3.up * -_gravityForce * 100f);
        }
    }

    public float ReturnSpeed()
    {
        return _accelerationSpeed;
    }

    public void DieFromSpeed()
    {
        Debug.Log("Die");
        _particles[2].Play();
        Die();
    }

    public void Die()
    {
        this.enabled = false;
        _visual.SetActive(false);
    }

    private void SmoothChangeValue(float target)
    {
        StartCoroutine(ChangeValue(target));
    }

    IEnumerator ChangeValue(float targetValue)
    {
        for (float t = 0; t < 1; t+= Time.deltaTime)
        {
            _velocityValueChange = Mathf.Lerp(_velocityValueChange, targetValue, t / 2);
        }
        yield return null;
    }
}
