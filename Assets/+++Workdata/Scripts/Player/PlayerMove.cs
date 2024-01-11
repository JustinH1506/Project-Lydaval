using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public Dictionary<string, SaveableVector3> positionsBySceneName = new Dictionary<string, SaveableVector3>();
    }
    
    #region Variables
    
    private float inputX;
    
    private float inputY;
    
    public int moveSpeed;
    
    #endregion

    #region Scripts

    private PlayerController playerController;
    [SerializeField] private Data positionData;

    #endregion

    #region Components

    Rigidbody2D rb;

    SpriteRenderer sr;

    Animator anim;

    #endregion

    #region Methods

    /// <summary>
    /// We get the Rigidbody2D, SpriteRenderer and Animator from the gameObject.
    /// playerController is a new PlayerController script. 
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

        playerController = new PlayerController();
    }

    private void Start()
    {
        var currentPosition = GameStateManager.instance.data.positionData;
        if (currentPosition != null)
        {
            positionData = currentPosition;
            
            GetPosition();
        }

        GameStateManager.instance.data.positionData = positionData;
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
    
    private void GetPosition()
    {
        if (positionData.positionsBySceneName.TryGetValue(gameObject.scene.name, out var position))
            transform.position = position;
    }
    
    /// <summary>
    /// We multiply the velocity with the inputX value and the moveSpeed and the inputY value times the moveSpeed. 
    /// </summary>
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * moveSpeed, inputY * moveSpeed);
    }

    private void LateUpdate()
    {
        var sceneName = gameObject.scene.name;
        if (!positionData.positionsBySceneName.ContainsKey(sceneName))
            positionData.positionsBySceneName.Add(sceneName, transform.position);
        else
            positionData.positionsBySceneName[sceneName] = transform.position;
    }

    /// <summary>
    /// We get the x value from context and the y value from context. 
    /// </summary>
    /// <param name="context"></param>
    private void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
    }
    #endregion
}
