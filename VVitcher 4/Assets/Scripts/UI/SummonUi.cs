using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonUi : MonoBehaviour {
    [SerializeField] private GameObject[] _herbValuesText;
    [SerializeField] private GameObject[] _herbInputValues;

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
        // Проверка на возможность призыва босса
        // Ничего не делать, если подношения нулевые
      
        // Если условия подношения не соблюдены, отнять у игрока 50% здоровья
        // Если условия подношения соблюдены, удалить использованные травы и призвать босса
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
