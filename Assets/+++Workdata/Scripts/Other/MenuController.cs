using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    private MenuControllerMap menuControllerMap;

    [SerializeField] private GameObject inventoryScreen;

    /// <summary>
    /// menuControllerMap is a new MenuControllerMap script.
    /// </summary>
    private void Awake()
    {
        menuControllerMap = new MenuControllerMap();
    }

    private void OnEnable()
    {
        menuControllerMap.Enable();
        
        menuControllerMap.Pause_Inventory.Inventroy.performed += InventoryScreen;
        menuControllerMap.Pause_Inventory.Inventroy.canceled += InventoryScreen;
    }

    private void OnDisable()
    {
        menuControllerMap.Disable();
        
        menuControllerMap.Pause_Inventory.Inventroy.performed -= InventoryScreen;
        menuControllerMap.Pause_Inventory.Inventroy.canceled -= InventoryScreen;
    }
    
    /// <summary>
    ///  We set the Inventory screen active if the button was pressed and its not active and set it off if
    /// the button was pressed and  its active. 
    /// </summary>
    private void InventoryScreen(InputAction.CallbackContext context)
    {
        if (context.performed && !inventoryScreen.activeSelf)
        {
            inventoryScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (context.performed)
        {
            inventoryScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
