using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    static SceneLoader instance;
    public static SceneLoader Instance { get { return instance; } }
    public event Action OnSceneLoad;
    public event Action OnFadeOut;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int idx)
    {
        StartCoroutine(LoadSceneFromIndex(idx));
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneFromIndex(int idx)
    {
        yield return UIManager.Instance.FadeOut(1.5f);
        OnFadeOut?.Invoke();
        yield return SceneManager.LoadSceneAsync(idx);
        OnSceneLoad?.Invoke();
        yield return UIManager.Instance.FadeIn(1.5f);
    }
}
