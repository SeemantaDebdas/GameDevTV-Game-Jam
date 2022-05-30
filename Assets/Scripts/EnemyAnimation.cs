using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    AIController enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<AIController>();
    }

    private void Update()
    {
        if (!enemy.IsDead)
        {
            if (Mathf.Abs(rb.velocity.x) > 0 && enemy.GetIsGrounded)
                anim.Play("Run");
        }
        else
            anim.SetBool("IsDead", true);
    }
}
