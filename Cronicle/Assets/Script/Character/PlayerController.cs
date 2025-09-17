using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    [Header("MoveMent")]
    public float moveSpeed;
    private Vector2 moveInput;
    public float rotateSpeed = 10f; // 회전 속도

    [Header("Jump")]
    public float jumpPower;
    public LayerMask groundLayerMarsk;

    [Header("Run")]
    public float runSpeed;

    [Header("Slide")]
    public bool isSliding;
    public float slideSpeed;

    [Header("Sit")]
    public bool isSitting;
    public float sitSpeed;

    [Header("Climb")]
    public bool climb;

    [Header("Gun")]
    public bool gunEquip;
    public bool isAiming;
    public bool isShooting;
    private float shootTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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
        Vector3 dir = new Vector3(moveInput.x, 0f, moveInput.y);

        if (dir.magnitude > 0.1f) // 입력이 있을 때만
        {
            // 이동
            Vector3 velocity = dir.normalized * moveSpeed;
            velocity.y = _rigidbody.velocity.y; // 중력 유지
            _rigidbody.velocity = velocity;

            // 회전 (이동 방향 바라보게)
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed);
        }
        else
        {
            // 입력 없으면 정지
            Vector3 velocity = _rigidbody.velocity;
            velocity.x = 0f;
            velocity.z = 0f;
            _rigidbody.velocity = velocity;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveInput = context.ReadValue<Vector2>();
            Vector2 lookInput = context.ReadValue<Vector2>();
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
            _animator.SetTrigger("Jump");
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
            Debug.DrawRay(rays[i].origin, rays[i].direction * 1f, Color.red);
            if (Physics.Raycast(rays[i], 0.5f, groundLayerMarsk))
                return true;
        }
        return false;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) 
            {
                // 뛰는 속도 증가
                Move();

                // 뛰기 시작
                _animator.SetTrigger("Run");
            }
        }
        Debug.Log("달리기 실행됨");

    }
}
