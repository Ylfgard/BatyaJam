using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    [SerializeField] private GameObject[] _herbValuesText;
    [SerializeField] private GameObject[] _boltValuesText;
    [SerializeField] private Transform[] _boltSprites;

    private PlayerInventory _inventory;

    void Start()
    {
        ResetInventoryUi();

        _inventory = PlayerInventory.instance;
        _inventory.onHerbInventoryChangedCallback += UpdateHerbInventoryUi;
        _inventory.onBoltInventoryChangedCallback += UpdateBoltInventoryUi;
        _inventory.onBoltChangedCallback += SwitchBolt;
    }

    private void UpdateHerbInventoryUi()
    {
        int[] herbs = _inventory.GetHerbs();
        for (int herbIndex = 0; herbIndex < herbs.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].GetComponent<TextMeshProUGUI>().text = herbs[herbIndex].ToString();
        }
    }

    private void UpdateBoltInventoryUi()
    {
        int[] bolts = _inventory.GetBolts();
        for (int boltIndex = 1; boltIndex < bolts.Length + 1; boltIndex++)
        {
            _boltValuesText[boltIndex].GetComponent<TextMeshProUGUI>().text = bolts[boltIndex].ToString();
        }
    }

    private void ResetInventoryUi()
    {
        for (int herbIndex = 0; herbIndex < _herbValuesText.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].GetComponent<TextMeshProUGUI>().text = "0";
        }

        for (int boltIndex = 1; boltIndex < _boltSprites.Length; boltIndex++)
        {
            _boltValuesText[boltIndex].GetComponent<TextMeshProUGUI>().text = "0";
        }
    }

    private void SwitchBolt(int activeBoltIndex)
    {
        _boltSprites[activeBoltIndex].SetAsLastSibling();
        _boltValuesText[activeBoltIndex].transform.SetAsLastSibling();
    }
}
