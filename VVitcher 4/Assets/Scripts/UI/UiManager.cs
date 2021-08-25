using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private GameObject _herbPanel;
    [SerializeField] private GameObject _boltPanel;
    [SerializeField] private GameObject _craftingPanel;
    [SerializeField] private GameObject _summonPanel;

    [SerializeField] private GameObject[] _herbValuesText;
    [SerializeField] private GameObject[] _boltValuesText;
    [SerializeField] private Transform[] _boltSprites;

    [SerializeField] private GameObject[] _herbValuesTextCraft;
    [SerializeField] private GameObject[] _boltValuesTextCraft;

    private PlayerInventory _inventory;
    private bool _inCraftMod;
    private bool _atSummonTable;

    public bool IsUsingUI()
    {
        return _inCraftMod || _atSummonTable;
    }

    public void ShowCursor(bool show)
    {
        if (show)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        ResetInventoryUi();

        _inCraftMod = false;

        _inventory = PlayerInventory.instance;
        _inventory.onHerbInventoryChangedCallback += UpdateHerbInventoryUi;
        _inventory.onBoltInventoryChangedCallback += UpdateBoltInventoryUi;
        _inventory.onBoltChangedCallback += SwitchBolt;

        _playerInput.onCraftModChangedCallback += SwitchCraftMod;
    }

    public void OpenSummonPanel(bool open)
    {
        if(_summonPanel.gameObject != null)
            _summonPanel.SetActive(open);

        ShowCursor(open);
        _atSummonTable = open;
    }

    private void UpdateHerbInventoryUi()
    {
        int[] herbs = _inventory.GetHerbs();
        for (int herbIndex = 0; herbIndex < herbs.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].GetComponent<TextMeshProUGUI>().text = herbs[herbIndex].ToString();
        }
        for (int herbIndex = 0; herbIndex < herbs.Length; herbIndex++)
        {
            _herbValuesTextCraft[herbIndex].GetComponent<TextMeshProUGUI>().text = herbs[herbIndex].ToString();
        }
    }

    private void UpdateBoltInventoryUi()
    {
        int[] bolts = _inventory.GetBolts();
        for (int boltIndex = 1; boltIndex < bolts.Length + 1; boltIndex++)
        {
            _boltValuesText[boltIndex].GetComponent<TextMeshProUGUI>().text = bolts[boltIndex-1].ToString();
        }
        for (int boltIndex = 0; boltIndex < bolts.Length; boltIndex++)
        {
            _boltValuesTextCraft[boltIndex].GetComponent<TextMeshProUGUI>().text = bolts[boltIndex].ToString();
        }
    }

    private void ResetInventoryUi()
    {
        for (int herbIndex = 0; herbIndex < _herbValuesText.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].GetComponent<TextMeshProUGUI>().text = "0";
        }
        for (int herbIndex = 0; herbIndex < _herbValuesText.Length; herbIndex++)
        {
            _herbValuesTextCraft[herbIndex].GetComponent<TextMeshProUGUI>().text = "0";
        }

        for (int boltIndex = 1; boltIndex < _boltSprites.Length; boltIndex++)
        {
            _boltValuesText[boltIndex].GetComponent<TextMeshProUGUI>().text = "0";
        }
        for (int boltIndex = 0; boltIndex < _boltSprites.Length - 1; boltIndex++)
        {
            _boltValuesTextCraft[boltIndex].GetComponent<TextMeshProUGUI>().text = "0";
        }
    }

    private void SwitchBolt(int activeBoltIndex)
    {
        _boltSprites[activeBoltIndex].SetAsLastSibling();
        _boltValuesText[activeBoltIndex].transform.SetAsLastSibling();
    }

    private void SwitchCraftMod()
    {
        _inCraftMod = !_inCraftMod;
        ShowCursor(IsUsingUI());

        _herbPanel.SetActive(!_inCraftMod);
        _boltPanel.SetActive(!_inCraftMod);
        _craftingPanel.SetActive(_inCraftMod);
    }
}
