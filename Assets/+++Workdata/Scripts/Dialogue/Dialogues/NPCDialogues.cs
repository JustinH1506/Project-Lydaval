using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCDialogues : MonoBehaviour
{
    private DialogueControllMap dialogueControllMap;
    
    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private TextAsset inkJSON;
    
    private bool inRange;

    private void Awake()
    {
        dialogueControllMap = new DialogueControllMap();
    }

    private void OnEnable()
    {
        dialogueControllMap.Enable();

        dialogueControllMap.Dialogue.Submit.performed += StartDialogue;
        dialogueControllMap.Dialogue.Submit.canceled += StartDialogue;
    }

    private void OnDisable()
    {
        dialogueControllMap.Disable();
        
        dialogueControllMap.Dialogue.Submit.performed -= StartDialogue;
        dialogueControllMap.Dialogue.Submit.canceled -= StartDialogue;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    
    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void StartDialogue(InputAction.CallbackContext context)
    {
        if (context.performed && inRange)
        {
            dialogueManager.EnterDialogueMode(inkJSON);
            inRange = false;
        }
    }
}
