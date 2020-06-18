using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public VoidEventSO jumpEvent;
    public Param1EventSO moveEvent;

    public float speed;
    public float jumpForce;

    Rigidbody rb;
    Collider col;

    const float OFFSET_RAYCAST = 0.1f;
    float colliderBoundaryY;

    const float GROUND_DISTANCE_X = 2f;
    const int NJumps = 1;

    int currentJumps;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Start()
    {
        rb.velocity = Vector3.forward * speed;
        colliderBoundaryY = col.bounds.extents.y;
        currentJumps = 0;

        jumpEvent.actionEvent += Jump;
        moveEvent.actionEvent += Move;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(true);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(false);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Move(bool right)
    {
        if (right)
        {
            if(Mathf.Abs(currentJumps + 1) <= NJumps)
            {
                transform.position += Vector3.right * GROUND_DISTANCE_X;
                currentJumps++;
            }
        }
        else
        {
            if (Mathf.Abs(currentJumps - 1) <= NJumps)
            {
                transform.position += Vector3.left * GROUND_DISTANCE_X;
                currentJumps--;
            }
        }
        
    }

    bool IsGrounded()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float distance = colliderBoundaryY + OFFSET_RAYCAST;

        RaycastHit hit;
        
        if (Physics.Raycast(origin, Vector3.down, out hit, distance))
        {
            if (hit.collider.CompareTag("Ground"))
                return true;
        }
        return false;
    }
}
