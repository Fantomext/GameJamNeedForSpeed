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

    [SerializeField] private float _defaultSpeed  = 50f;
    [SerializeField] private float _forwardAccel = 8f;
    [SerializeField] private float _reverseAccel = 4f;
    [SerializeField] private float _turnStrength = 180f;
    [SerializeField] private float _gravityForce = 10f;
    [SerializeField] private float _dragOnGround = 3f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _accelerationSpeed = 10f;
    [SerializeField] private float _nitroBar = 10f;
    [SerializeField] CinemachineVirtualCamera _cineMachine;

    private float _horizontal;
    private float _vertical;

    [SerializeField] private bool _grounded;

    [SerializeField] private float groundRayLength = 0.5f;
    [SerializeField] private Transform _groundRayPoint;
    private float _velocityValueChange;
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private AudioSource[] _audios;
    private bool _nitroIsActive = true;
    [SerializeField] private float _timer = 0;
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
            StartCoroutine(Nitro());
        }

        //if (Input.GetKeyDown(KeyCode.LeftShift) && _nitroBar > 3)
        //{

        //    _forwardAccel += _accelerationSpeed;
        //    SmoothChangeValue(0.3f);
        //    _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
        //    _particles[0].Play();
        //    _particles[1].Play();


        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    _nitroIsActive = false;
        //    _forwardAccel -= _accelerationSpeed;
        //    SmoothChangeValue(0f);
        //    _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
        //    _particles[0].Stop();
        //    _particles[1].Stop();
        //}
        

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

    IEnumerator Nitro()
    {
        if (_nitroIsActive)
        {
            _timer += Time.deltaTime;
            _nitroIsActive = false;
            _forwardAccel += _accelerationSpeed;
            SmoothChangeValue(0.3f);
            _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
            _particles[0].Play();
            _particles[1].Play();
            yield return new WaitForSeconds(3f);
            _nitroIsActive = false;
            _forwardAccel -= _accelerationSpeed;
            SmoothChangeValue(0f);
            _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
            _particles[0].Stop();
            _particles[1].Stop();
            yield return new WaitForSeconds(3f);
            _nitroIsActive = true;
        }
    }
        

    public void SetMaxSpeed()
    {
        _forwardAccel = _accelerationSpeed;
    }

    public void DefaultSpeed()
    {
        _forwardAccel = _defaultSpeed;
    }

    public void BoosterStart(int target)
    {
        StartCoroutine(Booster(target));
    }

    IEnumerator Booster(int target)
    {
        _forwardAccel += target;
        yield return new WaitForSeconds(3f);
        _forwardAccel -= target;
    }

    public float ReturnSpeed()
    {
        return _forwardAccel;
    }

    public float ReturnMagnitude()
    {
        return _rigibody.velocity.magnitude;
    }

    public void DieFromSpeed()
    {
        Debug.Log("Die");
        _particles[2].Play();
        Die();
    }

    public void Die()
    {
        _cineMachine.GetCinemachineComponent<CinemachineTransposer>().m_ZDamping = _velocityValueChange;
        _particles[0].Stop();
        _particles[1].Stop();
        this.enabled = false;
        _visual.SetActive(false);
        _audios[Random.Range(0,2)].Play();
        Invoke(nameof(Respawn), 1f);
    }
    [ContextMenu("Res")]
    public void Respawn()
    {
        this.enabled = true;
        _visual.SetActive(true);
        _rigibody.position = _spawnPosition.position;
        transform.rotation = _spawnPosition.rotation;
        Invoke(nameof(ResetVelocity), 0.5f);
        
    }

    public void ResetVelocity()
    {
        _rigibody.constraints = RigidbodyConstraints.FreezeAll;
        _rigibody.constraints = RigidbodyConstraints.None;
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
