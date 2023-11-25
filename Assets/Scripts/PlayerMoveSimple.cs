using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMoveSimple : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _targetDirection;
    private bool _isJump;
    [SerializeField] private bool _falling;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _velocity;
    [SerializeField] private bool _grounded;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _checkDistanceRange;

    private void Update()
    {
        Move();
        CheckGround();
        Jump();
        
    }

    public void Move()
    {
        transform.Translate(_targetDirection * _speed * Time.deltaTime, Space.Self);
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(transform.right * horizontal * _speed * Time.deltaTime, Space.Self);
    }

    public void Jump()
    {
        _velocity += Physics.gravity.y * _gravityScale * Time.deltaTime;

        if (_grounded)
        {
            _velocity = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            StartCoroutine(ChangeRange());
            _velocity = Mathf.Sqrt(_jumpForce * -2 * (Physics.gravity.y * _gravityScale));  
        }

        transform.Translate(transform.up * _velocity * Time.deltaTime, Space.Self);
    }

    public void SetRangeCheck(int value)
    {
        _checkDistanceRange = value;
    }

    IEnumerator ChangeRange()
    {
        _checkDistanceRange = 0;
        yield return new WaitForSeconds(0.5f);
        _checkDistanceRange = 1;
    }

    public void CheckGround()
    {
            Ray raycast = new Ray(transform.position, -transform.up);

            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit, _checkDistanceRange))
            {
                if (hit.collider != null)
                {
                    Vector3 surface = hit.point + transform.up * 1f;

                    transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hit.normal), Time.deltaTime);
                    
                    _grounded = true;
                }
            }
            else
            {
                _grounded = false;
            }
       
    }

}
