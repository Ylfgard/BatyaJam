using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour, IInteractable {
    [SerializeField] private PlayerMain player;
    [SerializeField] private int _healingPower;

    public void Interact()
    {
        // ��������� �� ������
        if (!player.isMaxHealth)
        {
            player.AbsorbHealingPowerup(_healingPower);
        }
        else
        {
            // �������� ������� � ���������?
        }

        Debug.Log("Interaction with medicine");

        Destroy(gameObject);
    }
}
