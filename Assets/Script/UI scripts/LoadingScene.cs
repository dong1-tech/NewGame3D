using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private Slider loadingSlider;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float process = Mathf.Clamp01(operation.progress / 0.9f);
            loadingSlider.value = process;
            yield return null;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
