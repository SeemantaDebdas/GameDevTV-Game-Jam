using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] List<Transform> waypointList;
    [SerializeField] float detectionRange = 2f;
    [SerializeField] float speed = 5f;
    [SerializeField] float groundCheckDistance = 1.0f;
    [SerializeField] LayerMask groundCheckLayerMask;
    [SerializeField] LayerMask detectionLayer;

    Rigidbody2D rb;
    Transform groundCheckTransform;
    Vector2 moveDir = Vector2.left;
    Vector2 lookDir;

    bool isDead = false;
    int currentWaypoint = 0;

    public bool IsDead { get { return isDead; } }

    private void Awake()
    {
        lookDir = -transform.right;
        rb = GetComponent<Rigidbody2D>();
        groundCheckTransform = transform.GetChild(0);
    }

    private void Update()
    {
        if (isDead) return;

        if (!IsGrounded() || ObstacleDetected() || ReachedWaypoint())
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            moveDir = -moveDir;
            lookDir = -lookDir;
            if(waypointList.Count > 0)
                currentWaypoint = (currentWaypoint + 1) % waypointList.Count;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir, detectionRange, detectionLayer);

        if (hit && hit.collider.CompareTag("Player"))
        {
            ThrowMolotov();
        }

        rb.velocity = moveDir * speed;
    }

    bool ReachedWaypoint()
    {
        if (waypointList.Count < 1) return false;
        
        if (Vector3.Distance(transform.position, waypointList[currentWaypoint].position) < 0.25f)
        {
            return true;
        }
        return false;
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(groundCheckTransform.position, Vector2.down, groundCheckDistance, groundCheckLayerMask))
            return true;
        return false;
    }

    private bool ObstacleDetected()
    {
        if (Physics2D.Raycast(groundCheckTransform.position, lookDir, groundCheckDistance, groundCheckLayerMask))
            return true;
        return false;
    }


    private void ThrowMolotov()
    {
        print("Molotov Thrown");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Interactable"))
        {
            Die();
            var interactableType = collision.gameObject.GetComponentInParent<Interactable>().GetInteractableType;
            if (interactableType == Interactable.InteractableType.Lever)
                Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        isDead = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (groundCheckTransform == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckTransform.position, (Vector2)groundCheckTransform.position + Vector2.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckTransform.position, (Vector2)groundCheckTransform.position + lookDir * groundCheckDistance);


        Gizmos.color = Color.red;
        Vector2 detectionDir = new Vector2(transform.position.x, transform.position.y + 0.2f);
        Gizmos.DrawLine(detectionDir, detectionDir + lookDir * detectionRange);
    }
}
