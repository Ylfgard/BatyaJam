using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonUi : MonoBehaviour {
    private int curDialogue = 0;
    private DialogueHandler dialogueHandler;
    [SerializeField] private BossPreset _bossPreset;
    [SerializeField] private TextAsset[] dialogue;
    [FMODUnity.EventRef] [SerializeField] private string[] dialoguePath; 
    [SerializeField] private GameObject[] _herbValuesText;
    [SerializeField] private GameObject[] _herbInputValues;

    private PlayerInventory _inventory;

    private void Start() {
        dialogueHandler = FindObjectOfType<DialogueHandler>();
    }

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
        int bloodyInput = Int32.Parse(_herbInputValues[0].GetComponent<TextMeshProUGUI>().text);
        int creackyInput = Int32.Parse(_herbInputValues[1].GetComponent<TextMeshProUGUI>().text);
        int linthyInput = Int32.Parse(_herbInputValues[2].GetComponent<TextMeshProUGUI>().text);

        if (bloodyInput == 0 & creackyInput == 0 & linthyInput == 0) return;
        // �������� �� ����������� ������� �����
        // ������ �� ������, ���� ���������� �������

        if (bloodyInput == _bossPreset.bloodyToSummon & creackyInput == _bossPreset.creackyToSummon & linthyInput == _bossPreset.linthyToSummon)
        {
            // �������� �����
            dialogueHandler.dialogueEnded.AddListener(SuccessSummon);
            dialogueHandler.StartDialogue(dialogue[curDialogue]);
            FMODUnity.RuntimeManager.PlayOneShotAttached(dialoguePath[curDialogue], gameObject);
        }
        else
        {
            PlayerMain player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
            player.TakeDamage((int)player.health / 2);
        }
    }

    void SuccessSummon()
    {
        curDialogue++;
        if(curDialogue < dialogue.Length)
        {
            dialogueHandler.StartDialogue(dialogue[curDialogue]);
            FMODUnity.RuntimeManager.PlayOneShotAttached(dialoguePath[curDialogue], gameObject);
        }
        else
        {
            dialogueHandler.dialogueEnded.RemoveListener(SuccessSummon);
            GameObject table = GameObject.Find("SummonTable");
            table.GetComponent<SummonTable>().boss.SetActive(true);
            Destroy(table);
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
