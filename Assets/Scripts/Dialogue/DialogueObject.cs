using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] 
    private string[] dialogue;

    public string[] Dialogue => dialogue;

    [SerializeField]
    private Response[] responses;

    public Response[] Responses => responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;
}
