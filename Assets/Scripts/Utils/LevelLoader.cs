using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float Progress { get; set; }

    void Start()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(GameManager.Instance.NewScene);

        while (!operation.isDone)
        {
            // Get round float 0.0 - 1.0
            Progress = Mathf.Round(operation.progress * 100)/100;
            yield return null;
        }
    }
}