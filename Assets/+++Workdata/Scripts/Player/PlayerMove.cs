using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    #region Variables

    private float inputX;

    private float inputY;

    public int moveSpeed;
    
    #endregion

    #region Scripts

    private PlayerController playerController;

    #endregion

    #region Components

    Rigidbody2D rb;

    SpriteRenderer sr;

    Animator anim;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        playerController = new PlayerController();
    }
    
    private void OnEnable()
    {
        playerController.Enable();

        playerController.Player.Movement.performed += Move;
        playerController.Player.Movement.canceled += Move;
    }

    private void OnDisable()
    {
        playerController.Disable();
        
        playerController.Player.Movement.performed -= Move;
        playerController.Player.Movement.canceled -= Move;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
    }

    private void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }
    #endregion
}
