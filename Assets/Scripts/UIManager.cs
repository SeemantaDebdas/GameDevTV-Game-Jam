using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [SerializeField] Canvas mainCanvas;
    [SerializeField] GameObject endCanvas;
    [SerializeField] TextMeshProUGUI textComp;

    CanvasGroup mainCanvasGroup;
    string message;
    float letterPause = 0.15f;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        endCanvas = transform.GetChild(0).gameObject;
        mainCanvasGroup = mainCanvas.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn(1f));
    }

    public IEnumerator FadeIn(float fadeTime)
    {
        while(mainCanvasGroup.alpha > 0)
        {
            mainCanvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float fadeTime)
    {
        while(mainCanvasGroup.alpha < 1)
        {
            mainCanvasGroup.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public void EnableEndCanvas()
    {
        endCanvas.SetActive(true);
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
