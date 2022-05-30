using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas;
    [SerializeField] TextMeshProUGUI textComp;

    CanvasGroup mainCanvasGroup;
    string message;
    float letterPause = 0.15f;
    void Start()
    {
        mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn(1f));
    }

    IEnumerator FadeIn(float fadeTime)
    {
        while(mainCanvasGroup.alpha > 0)
        {
            mainCanvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    IEnumerator FadeOut(float fadeTime)
    {
        while(mainCanvasGroup.alpha < 0)
        {
            mainCanvasGroup.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
    }


    IEnumerator TypeText()
    {
        yield return FadeIn(1f);
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            //if (typeSound1 && typeSound2)
            //    SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
