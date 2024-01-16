using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Vector2 walkDirection = Vector2.down;

    public bool isRunning;

    private Animator anim;

    public SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }

    public void SetWalkDirection(Vector2 newDirection)
    {
        if (newDirection.magnitude <= 0)
            return;
        
        walkDirection = newDirection;

        if(walkDirection.x < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
        
        anim.SetFloat("WalkDirectionX", walkDirection.x);
        anim.SetFloat("WalkDirectionY", walkDirection.y);
    }

    public void SetRun()
    {
        anim.SetBool("isRunning", isRunning);
        isRunning = true;
    }

    public void SetRunFalse()
    {
        anim.SetBool("isRunning", isRunning);
        isRunning = false;
    }
}
