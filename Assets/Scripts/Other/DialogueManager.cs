using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DialogueManager : MonoBehaviour
{
    public List<Text> texts;
    public float textDuration;

    private void Start()
    {
        StartCoroutine(fadeText());
    }

    public IEnumerator fadeText()
    {
        int counter = 0;
        while(counter < texts.Count)
        {
            StartCoroutine(fadeInText(counter));
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForSeconds(textDuration);
            StartCoroutine(fadeOutText(counter));
            yield return new WaitForSeconds(2.5f);
            counter++;
        }
        EventManager.Trigger("EndFade");
    }

    IEnumerator fadeInText(int index)
    {

        bool fadeComplete = false;
        float currentFade = 0;
        while (!fadeComplete)
        {
            currentFade += 0.01f;
            texts[index].color = new Color(texts[index].color.r, texts[index].color.g, texts[index].color.b, currentFade);

            if(currentFade >= 1)
            {
                fadeComplete = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator fadeOutText(int index)
    {

        bool fadeComplete = false;
        float currentFade = 1;
        while (!fadeComplete)
        {
            currentFade -= 0.01f;
            texts[index].color = new Color(texts[index].color.r, texts[index].color.g, texts[index].color.b, currentFade);

            if (currentFade <= 0)
            {
                fadeComplete = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
