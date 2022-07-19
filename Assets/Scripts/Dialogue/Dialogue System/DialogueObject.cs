using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea]
    private string[] _dialogue;

    [SerializeField]
    private Response[] _responses;

    //prevents code from the outside writting to it and makes it readable only
    public string[] Dialogue => _dialogue;

    
    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => _responses;
}
