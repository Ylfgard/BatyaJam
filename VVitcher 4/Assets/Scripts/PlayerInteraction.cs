using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        IInteractable item = other.GetComponent<IInteractable>();
        if (item == null) return;
        item.Interact();
    }
    private void OnTriggerExit(Collider other)
    {
        SummonTable summonTable = other.GetComponent<SummonTable>();
        if (summonTable == null) return;
        summonTable.CloseInteraction();
    }
}
