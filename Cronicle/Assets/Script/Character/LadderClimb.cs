using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [Header("Climb")]
    public bool isClimbing = false;
    public bool isEnterLadder = false;
    public bool isNearLadder = false;

    bool isUpLadder = false;
    bool isDownLadder = false;

    Vector3 ladderPosition;
    Vector3 ladderRotation;

    Animator animator;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        TryGetComponent(out animator);
    }

    public void CharacterToLadder() 
    {
        Vector3 targetPosition = new Vector3(ladderPosition.x, transform.position.y,ladderPosition.z);
        Vector3 dir = (targetPosition - transform.position).normalized;
        float distanceToLadder = Vector3.Distance(transform.position, targetPosition);
        characterController.Move(dir * distanceToLadder);

        //LookAtLadder();
        //LadderStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LadderDownStart"))
        {
            //NearLadderPodition(other);

            isUpLadder = true;
            isDownLadder = false;
        }

        else if (other.CompareTag("LadderDownEnd")) 
        {
            //EndOfLadder("LadderDownEnd");
        }

        else if (other.CompareTag("LadderUpStart")) 
        {
            //NearLadderPodition(other);

            isUpLadder = true;
            isDownLadder = false;
        }

        else if (other.CompareTag("LadderUpEnd"))
        {
            //EndOfLadder("LadderUpEnd");
        }
    }

    private void NearLadderPosition(Collider other) 
    { 
        isNearLadder = true;

        ladderPosition = other.gameObject.transform.position;
        ladderRotation = other.gameObject.transform.forward;
    }

    private void EndOfLadder() 
    { 
    
    }
}
