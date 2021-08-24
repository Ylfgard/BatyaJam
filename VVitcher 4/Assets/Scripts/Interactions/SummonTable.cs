using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummonTable : MonoBehaviour, IInteractable {
    public GameObject boss;
    private UiManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("GameUi").GetComponent<UiManager>();
    }

    public void Interact()
    {
        _uiManager.OpenSummonPanel(true);
    }

    public void CloseInteraction()
    {
        _uiManager.OpenSummonPanel(false);
    }

    private void OnDestroy() 
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            CloseInteraction();    
    }
}
