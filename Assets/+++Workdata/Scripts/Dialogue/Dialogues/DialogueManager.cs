using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    public bool dialogueIsPlaying { get; private set; }
    
    private Story currentStory;

    private DialogueControllMap dialogueControllMap;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    private void Awake()
    {
        dialogueControllMap = new DialogueControllMap();
    }

    private void OnEnable()
    {
        dialogueControllMap.Enable();

        dialogueControllMap.Dialogue.Submit.performed += Submit;
        dialogueControllMap.Dialogue.Submit.canceled += Submit;
    }

    private void OnDisable()
    {
        dialogueControllMap.Disable();
        
        dialogueControllMap.Dialogue.Submit.performed -= Submit;
        dialogueControllMap.Dialogue.Submit.canceled -= Submit;
    }

     public void EnterDialogueMode(TextAsset inkJSON)
     {
         currentStory = new Story(inkJSON.text);

         dialoguePanel.SetActive(true);
         
         dialogueIsPlaying = true;

         playerMove.enabled = false;
         
         currentStory.BindExternalFunction("Test2", (string secondTest) =>
         {
             Debug.Log(secondTest);
         });
         
         currentStory.BindExternalFunction("TestThis", (string textTest) =>
         {
             Debug.Log(textTest);
         });
     }
     
     private IEnumerator ExitDialogueMode()
     {
         yield return null;

         currentStory.UnbindExternalFunction("TestThis");
         
         currentStory.UnbindExternalFunction("Test2");
         
         dialogueIsPlaying = false;

         dialoguePanel.SetActive(false);

         playerMove.enabled = true;
     }
     
     public void ContinueStory()
     {
         if (currentStory.canContinue)
         {
             dialogueText.text = currentStory.Continue();
         }
         else
         {
             StartCoroutine(ExitDialogueMode());
         }
     }
     
     public void Submit(InputAction.CallbackContext context)
     {
         if (context.performed && dialogueIsPlaying)
         {
             ContinueStory();
         }
     }
}
