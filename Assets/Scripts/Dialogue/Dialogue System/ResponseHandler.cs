using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField]
    private RectTransform _responseBox;

    [SerializeField]
    private RectTransform _responseButtonTemplate;

    [SerializeField]
    private RectTransform _responseContainer;


    private DialogueUI _dialogueUI;

    List<GameObject> tempResponseButtons = new List<GameObject>();
   
    private void Start()
    {
        _dialogueUI = GetComponent<DialogueUI>();
    }


    //Shows the responses
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(_responseButtonTemplate.gameObject, _responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            tempResponseButtons.Add(responseButton);   

            responseBoxHeight += _responseButtonTemplate.sizeDelta.y;
        }

        _responseBox.sizeDelta = new Vector2(_responseBox.sizeDelta.x, responseBoxHeight);
        _responseBox.gameObject.SetActive(true);
    }


    //What happens when the response is clicked
    private void OnPickedResponse(Response response)
    {
        _responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();


        _dialogueUI.ShowDialogue(response.DialogueObject);
    }
}
