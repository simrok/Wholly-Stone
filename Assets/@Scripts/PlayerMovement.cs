using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    private Rigidbody rb;

    private Vector3 dir;
    public Vector3 Dir => dir;

    public bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = 6f;
        rotationSpeed = 300f;
        canMove = true;
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        dir = new Vector3(h, 0f, v).normalized;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        }

        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            Quaternion nextRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(nextRotation);
        }

    }

    public void ForceMove(Vector3 direcion, float speed)
    {
        rb.MovePosition(rb.position + direcion * speed * Time.fixedDeltaTime);
    }
}
