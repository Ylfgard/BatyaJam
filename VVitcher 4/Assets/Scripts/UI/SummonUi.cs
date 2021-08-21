using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonUi : MonoBehaviour {
    [SerializeField] private GameObject[] _herbValuesText;
    [SerializeField] private GameObject[] _herbInputValues;

    [SerializeField] private BossPreset _bossPreset;

    private PlayerInventory _inventory;

    public void AddValue(int index)
    {
        string textValue = _herbInputValues[index].GetComponent<TextMeshProUGUI>().text;
        int value = Int32.Parse(textValue);
        value++;

        if (value > _inventory.GetHerbs()[index]) return;

        _herbInputValues[index].GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void SubValue(int index)
    {
        string textValue = _herbInputValues[index].GetComponent<TextMeshProUGUI>().text;
        int value = Int32.Parse(textValue);
        value--;

        if (value < 0) return;

        _herbInputValues[index].GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void ResetInput()
    {
        foreach (var inputValue in _herbInputValues)
        {
            inputValue.GetComponent<TextMeshProUGUI>().text = "0";
        }
    }

    public void TrySummon()
    {
        int bloodyCount = Int32.Parse(_herbInputValues[0].GetComponent<TextMeshProUGUI>().text);
        int creackyCount = Int32.Parse(_herbInputValues[1].GetComponent<TextMeshProUGUI>().text);
        int linthyCount = Int32.Parse(_herbInputValues[2].GetComponent<TextMeshProUGUI>().text);

        if (bloodyCount == 0 & creackyCount == 0 & linthyCount == 0) return;

        if(bloodyCount == _bossPreset.bloodyToSummon && creackyCount == _bossPreset.creackyToSummon && linthyCount == _bossPreset.linthyToSummon)
        {
            int[] herbsToUse = new int[] { bloodyCount, creackyCount, linthyCount };
            _inventory.UseHerbs(herbsToUse);

            // Призвать босса

            GameObject summonTable = GameObject.Find("SummonTable");
            summonTable.GetComponent<SummonTable>().CloseInteraction();
            Destroy(summonTable);
        }
        else
        {
            // Отнять у игрока 50% здоровья
        }
    }

    private void UpdateHerbValues()
    {
        int[] herbs = _inventory.GetHerbs();
        for (int herbIndex = 0; herbIndex < herbs.Length; herbIndex++)
        {
            _herbValuesText[herbIndex].GetComponent<TextMeshProUGUI>().text = herbs[herbIndex].ToString();
        }
    }

    private void OnEnable()
    {
        _inventory = PlayerInventory.instance;

        UpdateHerbValues();
        ResetInput();
    }
}
