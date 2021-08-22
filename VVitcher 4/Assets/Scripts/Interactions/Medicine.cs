using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour, IInteractable {
    [SerializeField] private int _healingPower;

    private PlayerMain player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
    }

    public void Interact()
    {
        // ��������� �� ������
        if (!player.isMaxHealth)
        {
            player.AbsorbHealingPowerup(_healingPower);
            Destroy(gameObject);
        }
        else
        {
            // �������� ������� � ���������?
        }

        Debug.Log("Interaction with medicine");

        //Destroy(gameObject);
    }
}
