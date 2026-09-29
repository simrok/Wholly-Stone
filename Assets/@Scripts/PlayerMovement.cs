using UnityEditorInternal;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 15f;

    public bool isRunning;
    public bool IsRunning => isRunning;


    [SerializeField] private float rotationSmooth = 15f;

    private Rigidbody rb;

    private Vector3 dir;
    public Vector3 Dir => dir;

    public bool canMove;

    // 방향키 입력이 없으면 0, 뛰면 runSpeed, 걸으면 walkSpeed
    public float CurrentSpeed => (dir == Vector3.zero) ? 0 : ((isRunning == true) ? runSpeed : walkSpeed);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        dir = new Vector3(h, 0f, v).normalized;

        // 좌쉬프트 => 달리기 true
        if (Input.GetKeyDown(KeyCode.LeftShift)) { isRunning = !isRunning; }
        // 움직임을 멈추면 달리기 false
        if (dir == Vector3.zero) { isRunning = false; }
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;  // 물리 충돌로 생긴 회전을 매번 지움
        
        // 공격 / 구르기 중이면 움직임 + 회전을 막음
        if (!canMove) return;

        // canMove 상태이면
        Vector3 velocity = dir * CurrentSpeed;  // 수평 이동 속도
        velocity.y = rb.linearVelocity.y; // 위 아래는 중력에 맡김
        rb.linearVelocity = velocity;   // 물리 엔진이 이동 + 충돌 처리

        //Vector3 moveDir = dir;
        //// 발밑으로 레이를 쏴서 바닥의 기울기(법선)을 얻음
        //if (Physics.Raycast(rb.position + Vector3.up * 0.1f, Vector3.down, out RaycastHit hit, 0.5f))
        //{
        //    //Debug.DrawRay(rb.position + Vector3.up * 0.1f, Vector3.down * 0.5f, Color.green);
        //    //Debug.Log(hit.collider.name + " / normal: " + hit.normal);
        //    // 이동 방향을 바닥 면 위로 투영 -> 경사면을 따라 움직임
        //    moveDir = Vector3.ProjectOnPlane(dir, hit.normal).normalized;
        //}
        //// 플레이어 움직임 ( 걷기 / 달리기 )
        //rb.MovePosition(rb.position + moveDir * CurrentSpeed * Time.fixedDeltaTime);
        
        // 플레이어 회전
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            Quaternion nextRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSmooth * Time.fixedDeltaTime);
            rb.MoveRotation(nextRotation);
        }

    }

    public void ForceMove(Vector3 direcion, float speed)
    {
        rb.MovePosition(rb.position + direcion * speed * Time.fixedDeltaTime);
    }
}
