using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rb;
    Player player;

    Animator previousAnimator = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        //anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
        //anim.SetFloat("VerticalVelocity", player.GetVelocity);
        //anim.SetBool("IsGrounded", player.GetIsGrounded);
        //if (player.GetIsClimbing)
        //    anim.Play("ClimbAnim");

        if (player.GetIsGrounded)
        {
            if (Mathf.Approximately(player.GetInput.x, 0f))
                anim.Play("Idle");
            else if (!Mathf.Approximately(player.GetInput.x, 0f))
                anim.Play("Run");
        }
        else if (!player.GetIsGrounded)
        {
            if(FindAnimation(anim, "Fall") != null)
            {
                if (player.GetVelocity > 0f)
                    anim.Play("Jump");
                else if (player.GetVelocity < 0f)
                    anim.Play("Fall");
            }
            else
            {
                anim.Play("Jump");
            }
        }

    }

    public void ChangeAnimator(Animator anim)
    {
        previousAnimator = this.anim;
        this.anim = anim;
    }

    public void RevertAnimator() => this.anim = previousAnimator;

    public AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
