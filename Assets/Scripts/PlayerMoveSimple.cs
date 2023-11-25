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
    [SerializeField] private bool _cantMoveRight = false;
    [SerializeField] private bool _cantMoveLeft = false;
    [SerializeField] private List<Transform> _rayCastPositions = new List<Transform>();
    float horizontal = 0;

    private void Update()
    {
        Move();
        CheckGround();
        Jump();
        
    }

    public void Move()
    {
        transform.Translate(-transform.forward * _speed * Time.deltaTime, Space.World);
        horizontal = Input.GetAxisRaw("Horizontal");
        if (_cantMoveRight)
        {
            horizontal = Mathf.Clamp(horizontal, -1, 0);
        }
        else if (_cantMoveLeft)
        {
            horizontal = Mathf.Clamp(horizontal, 0, 1);
        }
        transform.Translate(transform.right * horizontal * _speed * Time.deltaTime, Space.Self);

    }

    public void CantMoveLeft()
    {
        _cantMoveLeft = true;
    }
    public void CanMoveLeft()
    {
        _cantMoveLeft = false;
    }

    public void CantMoveRight()
    {
        _cantMoveRight = true;
    }
    public void CanMoveRight()
    {
        _cantMoveRight = false;
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

    public void CheckGround()
    {
        Ray rayStart = new Ray(_rayCastPositions[0].position, -transform.up);
        Ray rayEnd = new Ray(_rayCastPositions[1].position, -transform.up);

        RaycastHit hitStart;
        RaycastHit hitEnd;

        if (Physics.Raycast(rayStart, out hitStart, _checkDistanceRange))
        {
            if (hitStart.collider != null)
            {
                Vector3 surface = hitStart.point + transform.up * 1f;

                transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
               // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hitStart.normal), Time.deltaTime);

                _grounded = true;
            }
        }
        else if (Physics.Raycast(rayEnd, out hitEnd, _checkDistanceRange))
        {
            if (hitEnd.collider != null)
            {
                Vector3 surface = hitEnd.point + transform.up * 1f;

                transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, hitEnd.normal), Time.deltaTime);

                _grounded = true;
            }
        }
        else
        {
            _grounded = false;
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        _speed = 0;

    }
    IEnumerator ChangeRange()
    {
        _checkDistanceRange = 0;
        yield return new WaitForSeconds(0.5f);
        _checkDistanceRange = 1;
    }

}
