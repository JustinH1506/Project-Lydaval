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

    /// <summary>
    /// Set anim to animator and sr to SpriteRenderer. 
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// we Set the walkDirection depending on the walkDirection. 
    /// </summary>
    /// <param name="newDirection"></param>
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

    /// <summary>
    /// We set the bool isRunning and set is running to true. 
    /// </summary>
    public void SetRun()
    {
        anim.SetBool("isRunning", isRunning);
        isRunning = true;
    }

    /// <summary>
    /// We set bool isRunning and isRunning to false. 
    /// </summary>
    public void SetRunFalse()
    {
        anim.SetBool("isRunning", isRunning);
        isRunning = false;
    }
}
