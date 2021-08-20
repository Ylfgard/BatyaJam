using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI[] _herbValuesText;

    private PlayerInventory _inventory;

    void Start()
    {
        ResetInventoryUi();

        _inventory = PlayerInventory.instance;
        _inventory.onInventoryChangedCallback += UpdateInventoryUi;
    }

    private void UpdateInventoryUi()
    {
        int[] herbs = _inventory.GetHerbs();

        for (int herbIndex = 0; herbIndex < herbs.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].text = herbs[herbIndex].ToString();
        }
    }

    private void ResetInventoryUi()
    {
        for (int herbIndex = 0; herbIndex < _herbValuesText.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].text = "0";
        }
    }
}
