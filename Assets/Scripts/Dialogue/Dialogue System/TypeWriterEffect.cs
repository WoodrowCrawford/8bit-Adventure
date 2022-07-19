using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField]
    private float _typewriterSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.6f },
        {new HashSet<char>(){',', ';', ':'}, 0.3f }
    };


    private Coroutine _typingCoroutine;
   

    //Runs the Coroutine
    public void Run(string textToType,TMP_Text textLabel)
    {
        _typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(_typingCoroutine);
        IsRunning = false;
    }

    //Types the text
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        IsRunning = true;
        textLabel.text = string.Empty;

        //elapsed time for writting
        float t = 0f; ;

        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;


            //Increases t overtime
            t += Time.deltaTime * _typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);


            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i + 1);

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        IsRunning = false;
    }


    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if (punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }
}
