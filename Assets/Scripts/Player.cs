using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float accelarationSpeed = 0.2f;
    [SerializeField] float decelarationSpeed = 0.5f;
    [SerializeField] float maxVelocity = 5f;

    [Header("Jumping")]
    [SerializeField] float maxJumpHeight = 3f;
    [SerializeField] float gravityMultiplier = 2f;
    [SerializeField] float groundCheckDistance = 1.0f;
    [SerializeField] LayerMask groundCheckLayerMask;

    [Header("Climbing")]
    [SerializeField] float climbingSpeed = 0.5f;
    
    Rigidbody2D rb = null;
    Vector3 velocity = Vector3.zero;
    Vector2 inputNormalized;
    bool isFacingLeft = false;
    bool isClimbing = false;
    bool stopInput = false;

    public bool GetIsGrounded { get { return IsGrounded(); } }
    public bool GetIsClimbing { get { return isClimbing; } }
    public float GetVelocity { get { return velocity.y; } }
    public Vector2 GetInput { get { return inputNormalized; } }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stopInput) return;

        inputNormalized = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        //movement condition check
        if(Mathf.Abs(inputNormalized.x) > 0.1f)
        {
            float maxVelocityX;
            if(inputNormalized.x > 0)
            {
                maxVelocityX = maxVelocity;
                isFacingLeft = false;
            }
            else
            {
                maxVelocityX = -maxVelocity;
                isFacingLeft = true;
            }
            FlipSprite(isFacingLeft);
            velocity.x = Mathf.MoveTowards(velocity.x, maxVelocityX, accelarationSpeed);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, decelarationSpeed);
        }

        //Grounded condition check
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(-2* Physics2D.gravity.y * gravityMultiplier * maxJumpHeight);
        }
        else if(!isClimbing)
        {
            velocity.y = rb.velocity.y; 
        }

        //climbing
        if (isClimbing)
        {
            velocity.y = inputNormalized.y * climbingSpeed;
        }

        rb.velocity = new Vector2(velocity.x, velocity.y); 
    }

    public void ControlInput(bool state) => stopInput = state;

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, groundCheckDistance, groundCheckLayerMask))
            return true;
        return false;
    }

    void FlipSprite(bool isFacingLeft)
    {
        Vector3 localScale = transform.localScale;
        if (isFacingLeft)
            localScale.x = -Mathf.Abs(localScale.x);
        else
            localScale.x = Mathf.Abs(localScale.x);

        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder") && !isFacingLeft)
        {
            isClimbing = true;
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.isKinematic = false;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
