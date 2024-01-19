using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Ink.Runtime;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    public bool dialogueIsPlaying { get; private set; }
    
    private Story currentStory;

    private DialogueControllMap dialogueControllMap;

    [SerializeField] private PlayerMove playerMove;

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private PlayableDirector blackScreenFadeIn, forestEntry;

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

         playerMove.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

         playerMove.enabled = false;
         
         currentStory.BindExternalFunction("activateCutscene", (string activate) =>
         {
             blackScreenFadeIn.Play();

             StartCoroutine(ExitDialogueMode());
         });
         
         currentStory.BindExternalFunction("playResume", (string resume) =>
         {
             forestEntry.Resume();

             StartCoroutine(ExitDialogueMode());
         });
         
         ContinueStory();
     }
     
     private IEnumerator ExitDialogueMode()
     {
         yield return null;
         
         dialogueIsPlaying = false;

         dialoguePanel.SetActive(false);
         
         currentStory.UnbindExternalFunction("activateCutscene");
         
         currentStory.UnbindExternalFunction("playResume");

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