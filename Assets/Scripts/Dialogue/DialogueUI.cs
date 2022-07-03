using UnityEngine;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textLabel;

    private void Start()
    {
        GetComponent<TypewriterEffect>().Run("This is how you make something in a game.\nAlso I have no idea where I am.", textLabel);
    }
}
