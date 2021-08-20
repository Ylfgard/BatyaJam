using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour, IInteractable {
    [SerializeField] private int _healingPower;

    public void Interact()
    {
        // Увеличить хп игрока

        Debug.Log("Interaction with medicine");

        Destroy(gameObject);
    }
}
