using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [Header("InputEvents")]
    public VoidEventSO jumpEvent;
    public Param1BoolEventSO moveEvent;

    public VoidEventSO onGameOver;

    [Header("Parameters")]
    public float speed;
    public float jumpForce;

    Rigidbody rb;
    Collider col;

    const float OFFSET_RAYCAST = 0.3f;
    float colliderBoundaryY;

    const float GROUND_DISTANCE_X = 2f;
    const int NJumps = 1;

    const float ROTATION_SPEED = 10f;

    const float SPEED_INCREASE = 0.75f;
    const float MAX_SPEED = 150f;

    int currentJumps;
    bool isMoving;
    float newPosX;
    float dampVelocity = 0f;

    CubeTweenAnimations cubeTweenAnimations;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Start()
    {
        rb.velocity = Vector3.forward * speed;
        colliderBoundaryY = col.bounds.extents.y;
        cubeTweenAnimations = new CubeTweenAnimations(transform);

        EnableEvents();
    }


    void Update()
    {
        Rotate();
        DoMovement();
        speed = Mathf.Clamp(speed + SPEED_INCREASE * Time.deltaTime, 0f, MAX_SPEED);
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);

    }


    void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Move(bool right)
    {
        if (!isMoving)
        {
            isMoving = true;
            if (right && Mathf.Abs(currentJumps + 1) <= NJumps)
            {
                newPosX = transform.position.x + GROUND_DISTANCE_X;
                currentJumps++;
                cubeTweenAnimations.DoTweensMovement(right, IsGrounded());


            }
            else if(!right && Mathf.Abs(currentJumps - 1) <= NJumps)
            {
                newPosX = transform.position.x - GROUND_DISTANCE_X;
                currentJumps--;
                cubeTweenAnimations.DoTweensMovement(right, IsGrounded());
            }
        }
        
    }

    void Rotate()
    {

        transform.Rotate(Vector3.right * speed * ROTATION_SPEED * Time.deltaTime, Space.World);

    }

    void DoMovement()
    {
        if (isMoving)
        {
            float x = Mathf.SmoothDamp(transform.position.x, newPosX, ref dampVelocity, 0.2f);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            if (Mathf.Abs(transform.position.x - newPosX) <= 0.05f)
            {
                transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
                isMoving = false;   
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

    void OnDisable()
    {
        DisableEvents();
    }

    void EnableEvents()
    {
        jumpEvent.actionEvent += Jump;
        moveEvent.actionEvent += Move;
    }

    void DisableEvents()
    {
        jumpEvent.actionEvent -= Jump;
        moveEvent.actionEvent -= Move;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
            onGameOver.InvokeEvent();
                //Time.timeScale = 0f;
    }

}
