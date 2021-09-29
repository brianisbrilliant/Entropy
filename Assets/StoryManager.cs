using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* 
        Here at the end, what will I be doing?
        I don't want to be fighting over limited resources.
        I want to enjoy the time that I have.
        I want to spend my time with friends and family.
        I want to be exploring.
        I want to be creating.
     */


public class StoryManager : MonoBehaviour
{
    public string[] lines = new string[] { "Here at the end, what will I be doing?",
                                    "I don't want to be fighting over limited resources.",
                                    "I want to enjoy the time that I have.",
                                    "I want to spend my time with friends and family.",
                                    "I want to be exploring.",
                                    "I want to be creating."
        };

    public TextMeshProUGUI storyText;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowLines());
    }

    IEnumerator ShowLines()
    {
        while(index != lines.Length)     // 15 seconds per loop.
        {
            
            yield return new WaitForSeconds(1);     // fade in text.
            storyText.text = lines[index];
            // show text
            yield return new WaitForSeconds(10);    // let the text be read.
            if(index + 1 != lines.Length)
            {
                yield return new WaitForSeconds(1);    // fade out text.
                storyText.text = "";
                // change text

                
                yield return new WaitForSeconds(3);     // pause for silence.
            }
            index++;
        }
    }
}
