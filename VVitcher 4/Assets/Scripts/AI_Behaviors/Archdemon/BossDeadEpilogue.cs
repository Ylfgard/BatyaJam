using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeadEpilogue : MonoBehaviour
{
    [SerializeField]
    private float finalSceneLoadDelay = 10f;

    private float timer;

    private void LoadEpilogue() => StartCoroutine(LoadEpilogueCoroutine());

    IEnumerator LoadEpilogueCoroutine()
    {
        yield return new WaitForSeconds(finalSceneLoadDelay);
        SceneManager.LoadScene(3);
    }
}
