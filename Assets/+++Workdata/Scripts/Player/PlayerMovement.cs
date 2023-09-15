using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    #region Tilemaps

    [SerializeField] private Tilemap groundTilemap;

    [SerializeField] private Tilemap collisionTilemap;

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
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    /// <summary> Using the input and giving context and the Movement Value </summary>
    void Start()
    {
        playerController.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    /// <summary> If Moveable moving to another tile</summary>
 
    public void Move(Vector2 direction)
    {
        if(CanMove(direction))
            transform.position += (Vector3)direction;
    }

    /// <summary> Making teh gridposition to the groundTilemap to World Cell + the dircetion in Vector 3. 
    /// asking if groundTilemap does not have a Tile or collisionTilemap has a tile return false. else return true.
    /// </summary>
    public bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);

        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
    #endregion
}