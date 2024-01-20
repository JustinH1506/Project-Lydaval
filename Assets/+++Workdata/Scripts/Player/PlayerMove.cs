using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerMove : MonoBehaviour
{
    #region Local classes
    [System.Serializable]
    public class Data
    {
        public Dictionary<string, SaveableVector3> positionsBySceneName = new Dictionary<string, SaveableVector3>();

        public bool startCutsceneOff, burningHousesOn;
    }
    
    #endregion 
    
    #region Variables
    
    private float inputX;
    
    private float inputY;

    private float walkDirectionX, walkDirectionY;
    
    public int moveSpeed;

    [SerializeField] private GameObject startCutscene;

    [SerializeField] private PlayerAnimator playerAnimator;
    
    #endregion

    #region Scripts

    private PlayerController playerController;
    public Data positionData;

    #endregion

    #region Components

    public Rigidbody2D rb;

    SpriteRenderer sr;
    
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

        playerAnimator = GetComponentInChildren<PlayerAnimator>();

        playerController = new PlayerController();
    }

    private void Start()
    {
        var currentPosition = GameStateManager.instance.data.positionData;
        if (currentPosition != null)
        {
            positionData = currentPosition;
            
            GetPosition();

            if (positionData.startCutsceneOff)
                startCutscene.SetActive(false);
        }

        GameStateManager.instance.data.positionData = positionData;
        
        if(positionData.startCutsceneOff == false)
            startCutscene.GetComponent<PlayableDirector>().Play();
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
        
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            playerAnimator.SetRun();
            playerAnimator.SetWalkDirection(rb.velocity);
        }
        else
        {
            playerAnimator.SetRunFalse();
        }
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