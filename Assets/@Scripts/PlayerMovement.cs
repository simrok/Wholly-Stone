using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSmooth;

    private Rigidbody rb;

    private Vector3 dir;
    public Vector3 Dir => dir;

    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 10f;
        rotationSmooth = 300f;
        canMove = true;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0f, v).normalized;
    }

    private void FixedUpdate()
    {
        // 플레이어 움직임
        if (canMove)
        {
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }
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
