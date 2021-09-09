using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossDeadEpilogue : MonoBehaviour
{
    [SerializeField] private float finalSceneLoadDelay = 10f;

    [SerializeField] private Image _fadeOutImage;
    [SerializeField] private float _fadeOutStart = 5f;
    [SerializeField] private float _fadeOutTime = 3f;

    private float currentAlpha = 0;
    private float t;

    private void LoadEpilogue()
    {
        StartCoroutine(LoadEpilogueCoroutine());
        StartCoroutine(FadeOutStartCoroutine());
    }

    IEnumerator LoadEpilogueCoroutine()
    {
        yield return new WaitForSeconds(finalSceneLoadDelay);
        SceneManager.LoadScene(3);
    }
    
    IEnumerator FadeOutStartCoroutine()
    {
        yield return new WaitForSeconds(_fadeOutStart);
        StartCoroutine(FadeOutCoroutine());
        Debug.Log("Start Fade Out.");
    }
    IEnumerator FadeOutCoroutine()
    {
        t += Time.deltaTime / _fadeOutTime;
        currentAlpha = Mathf.Lerp(0, 1, t);

        _fadeOutImage.color = new Color(0, 0, 0, currentAlpha);

        if (currentAlpha >= 1) yield break;
        yield return null;

        StartCoroutine(FadeOutCoroutine());
    }
}
