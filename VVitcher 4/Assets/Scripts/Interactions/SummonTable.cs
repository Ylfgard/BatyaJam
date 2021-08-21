using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTable : MonoBehaviour, IInteractable {
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
}
