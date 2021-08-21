using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour, IInteractable {
    [SerializeField] private PlayerMain player;
    [SerializeField] private int _healingPower;

    public void Interact()
    {
        // Увеличить хп игрока
        if (!player.isMaxHealth)
        {
            player.AbsorbHealingPowerup(_healingPower);
        }
        else
        {
            // Положить аптечку в инвентарь?
        }

        Debug.Log("Interaction with medicine");

        Destroy(gameObject);
    }
}
