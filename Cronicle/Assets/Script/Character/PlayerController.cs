using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("MoveMent")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 moveInput;
    private Rigidbody _rigidbody;
    public LayerMask groundLayerMarsk;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate() 
    {
        Move();
    }

    public void Move() 
    {
        Debug.DrawRay(transform.position + (transform.forward * 0.2f) + (transform.up * 0.3f), Vector3.down * 0.5f, Color.red, 0.1f);
        Vector3 dir = transform.forward * moveInput.y + transform.right * moveInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;
        _rigidbody.velocity = dir;
        Debug.Log(dir);
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag())
    //    {
    //        Debug.Log("플레이어 터치");
    //    }
    //}

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            Debug.Log("점프 실행됨");
        }
        Debug.Log(IsGrounded());
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
          new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.3f),Vector3.down),
          new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.3f),Vector3.down),
          new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.3f),Vector3.down),
          new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.3f),Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.4f, Color.red);
            if (Physics.Raycast(rays[i], 1f, groundLayerMarsk))
                return true;
        }
        return false;
    }
}
