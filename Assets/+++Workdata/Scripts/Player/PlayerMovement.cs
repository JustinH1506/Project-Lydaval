using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] float moveSpeed;

    private float inputX, inputY;

    #endregion 

    #region Scripts

    PlayerController playerController;

    #endregion

    #region Components

    Rigidbody2D rb;

    SpriteRenderer sr;

    Animator anim;

    #endregion

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

        playerController.Player.Move.performed += Movement;
        playerController.Player.Move.canceled += Movement;
    }

    private void OnDisable()
    {
        playerController.Disable();

        playerController.Player.Move.performed -= Movement;
        playerController.Player.Move.canceled -= Movement;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);  
    }

    public void Movement(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }
}