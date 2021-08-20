using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HerbType {
    bloody = 0,
    creaky = 1,
    linthy = 2
}

public class Herb : MonoBehaviour, IInteractable {
    [SerializeField] private HerbType type;

    public void Interact()
    {
        PlayerInventory.instance.AddHerb(type);

        Debug.Log($"Interaction with herb type: {type}");

        Destroy(gameObject);
    }
}
