using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    MainBtn mainBtn = new MainBtn();
    private Rigidbody _rigidbody;
    private Animator _animator;
    public CameraMover cameraMover;

    [Header("Start")]
    UIManager uiManager;
    public bool isStanding = false;

    [Header("Move")]
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

    [Header("Interact")]
    [SerializeField] PuzzleManager puzzle;
    public float interactDistance = 3f; // 상호작용 거리
    public LayerMask interactLayer;     // 퍼즐 레이어 지정 가능 (선택)
    public bool openDoor;
    public bool closeDoor;

    [Header("Gun")]
    [SerializeField] private GameObject gun;
    public bool gunEquip;
    public bool isAiming;
    public bool isShooting;
    public bool hasGun = false;

    [Header("GravityOption")]
    [SerializeField] private GameObject gravityOptionUI;

    [Header("Candle")]
    [SerializeField] private GameObject candle;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        if (isStanding)
        {
            Move(); //구상에 맞게 조립하는 연습
        }

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
            Debug.Log(_rigidbody.velocity);
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

    public void OnStandUp()
    {
        StartCoroutine(StandUpAfterDelay());
        _animator.SetBool("Idle", true);
    }

    private IEnumerator StandUpAfterDelay() 
    {
        _animator.SetBool("IsRepose", true);
        isStanding = false;

        yield return new WaitForSeconds(3f); // 3초 대기

        _animator.SetBool("IsRepose", false);
        _animator.SetBool("IsStandUp", true);

        yield return new WaitForSeconds(6f);

        _animator.SetBool("IsStandUp", false);
        isStanding = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (!isStanding) return; // 서있지 않으면 움직이지 않음

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
          new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.3f),Vector3.down),
          new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.3f),Vector3.down),
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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 플레이어 시점에서 앞으로 Ray 발사
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
            {
                PuzzleManager puzzle = hit.collider.GetComponent<PuzzleManager>();
                if (puzzle != null)
                {
                    Debug.Log("퍼즐과 상호작용!");
                    //퍼즐의 코루틴 실행
                    //StartCoroutine(puzzle.RotationValue());
                    //StartCoroutine(puzzle.OpenDoor());
                }
            }
        }
    }

    public void OnGravityOption(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started) 
        {
            if (!gravityOptionUI.activeSelf)
                gravityOptionUI.SetActive(true);
            else
                gravityOptionUI.SetActive(false);
        }
    }

    public void OnGun(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started)
        {
            hasGun = true;
            gun.SetActive(true);
            _animator.SetTrigger("IsGunTrigger");
        }
    }

    public void OnCandle(InputAction.CallbackContext context) 
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(!candle.activeSelf)
                candle.SetActive(true);

            else
                candle.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            cameraMover.NextStage();
        }
    }
}
