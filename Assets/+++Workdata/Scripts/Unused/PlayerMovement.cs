using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    /// <summary> 
    /// If Moveable moving to another tile
    /// </summary>
    private void Move(Vector2 direction)
    {
        if (direction.x != 0)
            direction.x = Mathf.Sign(direction.x);
        if (direction.y != 0)
            direction.y = Mathf.Sign(direction.y);
        
        if (CanMove(direction))
            transform.position += (Vector3)direction;
    }

    /// <summary> Making teh Gridposition to the groundTilemap to World Cell + the direction in Vector 3. 
    /// Asking if groundTilemap does not have a Tile or collisionTilemap has a tile return false. Else return true.
    /// </summary>
    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);

        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }

    #endregion
}