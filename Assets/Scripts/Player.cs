using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float accelarationSpeed = 0.2f;
    [SerializeField] float decelarationSpeed = 0.5f;
    [SerializeField] float maxVelocity = 5f;
    [SerializeField] float maxJumpHeight = 3f;
    [SerializeField] float gravityMultiplier = 2f;
    [SerializeField] float groundCheckDistance = 1.0f;
    [SerializeField] LayerMask groundCheckLayerMask;
    
    Rigidbody2D rb = null;
    Vector3 velocity = Vector3.zero;
    bool isFacingLeft = false;

    public bool GetIsGrounded { get { return IsGrounded(); } }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputNormalized = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;

        if(inputNormalized.magnitude > 0.1f)
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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            velocity.y = Mathf.Sqrt(-2* Physics2D.gravity.y * gravityMultiplier * maxJumpHeight);
        }
        else
        {
            velocity.y = rb.velocity.y; 
        }

        rb.velocity = new Vector2(velocity.x, velocity.y); 
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
