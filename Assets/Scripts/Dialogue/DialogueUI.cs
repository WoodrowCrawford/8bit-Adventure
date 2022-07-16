using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textLabel;

    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField]
    private DialogueObject testDialogue;

    private ResponseHandler _responseHandler;
    private TypewriterEffect typewriterEffect;

    public InputHandler controls;

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
        _responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.Responses != null && dialogueObject.HasResponses) break;

            yield return new WaitUntil(() => controls.GetComponent<InputHandler>().GoToNextSentence == true);
        }

        if (dialogueObject.HasResponses)
        {
            _responseHandler.ShowResponses(dialogueObject.Responses);

        }
        else
        {
            CloseDialogueBox();
        }

 
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

}
