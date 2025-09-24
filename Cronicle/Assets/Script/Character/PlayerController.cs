using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    MainBtn mainBtn = new MainBtn();
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
    public float slideSpeed;

    [Header("Sit")]
    public float sitSpeed;

    [Header("Climb")]
    public bool climb;

    [Header("Gun")]
    public bool gunEquip;
    public bool isAiming;
    public bool isShooting;
    public bool hasGun = false;
    private float shootTimer;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
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

            // 뛰기
            if (_animator.GetBool("IsRunning"))
            {
                velocity = dir.normalized * runSpeed;
                velocity.y = _rigidbody.velocity.y; // 중력 유지
                _rigidbody.velocity = velocity;
            }
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
            _animator.SetBool("IsWalk", true); // 걷기 시작
            Debug.Log("걷기 실행됨");
        }

        else if (context.phase == InputActionPhase.Canceled)
        {
            moveInput = Vector2.zero;
            _animator.SetBool("IsWalk", false); // 걷기 시작
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            Debug.Log("점프 실행됨");
            _animator.SetTrigger("IsJump");
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
            // 뛰기 시작
            _animator.SetBool("IsRunning", true);
            Debug.Log("달리기 실행됨");
        }
        else
        {     // 뛰기 멈춤
            _animator.SetBool("IsRunning", false);
            Debug.Log("달리기 실행안됨");
        }
    }

    public void OnSit(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 앉기 시작
            _animator.SetBool("IsSit", true);
        }
        else 
        {
            // 앉기 멈춤
            _animator.SetBool("IsSit", false);
        }
    }

    public void OnSlider(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 슬라이드 시작
            _animator.SetBool("IsSlider", true);
        }
        else 
        {
            // 슬라이드 멈춤
            _animator.SetBool("IsSlider", false);
        }
    }

    public void OnGun(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Performed)
        {
            hasGun = !hasGun;
            _animator.SetBool("hasGun", hasGun);

            // 중력건 포즈 시작
            if (hasGun)
            _animator.SetBool("IsGun", true);
        }

        else 
        {
            // 중력건 포즈 취소
            _animator.SetBool("IsGun", false);
        }
    }

    public Transform rightHandGrip;

    void OnAnimatorIK(int layerIndex)
    {
        if (_animator.GetBool("HasGun"))
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandGrip.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandGrip.rotation);
        }
    }
}
