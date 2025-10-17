using UnityEngine;

namespace GogoGaga.OptimizedRopesAndCables
{
    [RequireComponent(typeof(RopeMesh))]
    public class RopeRideInteraction : MonoBehaviour
    {
        public KeyCode interactKey = KeyCode.E;
        public float rideSpeed = 5f;   // 이동 속도 (m/s)
        public float stopDistance = 0.5f;

        private Rope rope;
        private RopeMesh ropeMesh;
        private Transform player;
        private CharacterController controller;

        private bool isNearRope = false;
        private bool isRiding = false;
        private float rideT = 0f;  // 로프를 따라 이동할 위치 (0~1)

        void Start()
        {
            ropeMesh = GetComponent<RopeMesh>();
            rope = GetComponent<Rope>();
        }

        void Update()
        {
            if (player == null)
            {
                // 근처에 있는 Player 찾기 (단, Find 비효율이라 실제 게임에서는 Trigger로 처리 추천)
                GameObject playerObj = GameObject.FindWithTag("Player");
                if (playerObj != null)
                {
                    player = playerObj.transform;
                    controller = playerObj.GetComponent<CharacterController>();
                }
                return;
            }

            // 로프와 플레이어 거리 확인
            float distToRope = Vector3.Distance(player.position, rope.StartPoint.position);
            float distToEnd = Vector3.Distance(player.position, rope.EndPoint.position);
            isNearRope = (distToRope < 3f || distToEnd < 3f);

            // E키 입력
            if (isNearRope && !isRiding && Input.GetKeyDown(interactKey))
            {
                StartRide(distToRope < distToEnd ? 0f : 1f);
            }

            if (isRiding)
            {
                UpdateRide();
                if (Input.GetKeyDown(interactKey))
                {
                    StopRide();
                }
            }
        }

        void StartRide(float startT)
        {
            isRiding = true;
            rideT = startT;
            // 중력 끄기 (CharacterController 기준)
            if (controller != null)
            {
                controller.enabled = false;
            }
        }

        void StopRide()
        {
            isRiding = false;
            if (controller != null)
            {
                controller.enabled = true;
            }
        }

        void UpdateRide()
        {
            if (rope == null || player == null) return;

            Vector3 start = rope.StartPoint.position;
            Vector3 end = rope.EndPoint.position;

            Vector3 ropeDir = (end - start).normalized;
            float ropeLength = Vector3.Distance(start, end);

            // 방향 결정 (시작→끝 or 끝→시작)
            float direction = (rideT < 0.5f) ? 1f : -1f;

            rideT += (rideSpeed / ropeLength) * Time.deltaTime * direction;
            rideT = Mathf.Clamp01(rideT);

            // 로프 상 위치
            Vector3 targetPos = rope.GetPointAt(rideT);
            player.position = targetPos;

            // 로프 방향으로 플레이어 회전
            player.forward = Vector3.Lerp(player.forward, ropeDir, Time.deltaTime * 5f);

            // 도착 시 자동 하차
            if (rideT <= 0f + stopDistance / ropeLength || rideT >= 1f - stopDistance / ropeLength)
            {
                StopRide();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (isNearRope)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, 3f);
            }
        }
    }
}
