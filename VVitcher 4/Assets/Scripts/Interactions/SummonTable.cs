using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummonTable : MonoBehaviour, IInteractable {
    public GameObject boss;
    private UiManager _uiManager;
    private CameraModeChanger _cameraModeChanger;

    private void Start()
    {
        _uiManager = GameObject.FindGameObjectWithTag("GameUi").GetComponent<UiManager>();
        _cameraModeChanger = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraModeChanger>();
    }

    public void Interact()
    {
        _uiManager.OpenSummonPanel(true);
        _cameraModeChanger.SetPlayerWalkMode();
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
