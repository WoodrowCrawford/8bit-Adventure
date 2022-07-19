using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _dialogueBox;

    [SerializeField]
    private TMP_Text _textLabel;

    private ResponseHandler _responseHandler;
    private TypeWriterEffect _typeWriterEffect;

    public bool isOpen { get; private set; }



    private void Start()
    {
        _typeWriterEffect = GetComponent<TypeWriterEffect>();
        _responseHandler = GetComponent<ResponseHandler>();

        CloseDialogueBox();
    }


    //Shows the dialogue 
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;
        _dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }


    //Steps through the dialogue 0
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
       
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++ )
        {
            string dialogue = dialogueObject.Dialogue[i];
            
            yield return RunTypingEffect(dialogue);

            _textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Keyboard.current.eKey.IsPressed());
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


    private IEnumerator RunTypingEffect(string dialogue)
    {
        _typeWriterEffect.Run(dialogue, _textLabel);

        while (_typeWriterEffect.IsRunning)
        {
            yield return null;

            if(Keyboard.current.tabKey.IsPressed())
            {
                _typeWriterEffect.Stop();
            }
        }
    }

    //Closes the dialogue box
    private void CloseDialogueBox()
    {
        isOpen = false;
        _dialogueBox.SetActive(false);
        _textLabel.text = string.Empty;
    }
}
