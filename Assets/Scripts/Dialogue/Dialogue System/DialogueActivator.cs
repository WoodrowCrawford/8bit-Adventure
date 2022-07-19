using UnityEngine;

public class DialogueActivator : MonoBehaviour , IInteractable
{
    [SerializeField]
    private DialogueObject _dialogueObject;

   


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementBehavior player))
        {
            player.Interactable = this;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.TryGetComponent(out PlayerMovementBehavior player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerMovementBehavior player)
    {
        player.DialogueUI.ShowDialogue(_dialogueObject);
    }
}
