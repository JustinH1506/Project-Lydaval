using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    #region

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

    void Start()
    {
        playerController.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    public void Move(Vector2 direction)
    {
        if(CanMove(direction))
            transform.position += (Vector3)direction;
    }

    public bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);

        if (!groundTilemap.HasTile(gridPosition) || collisionTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
}