using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public delegate void OnCraftModChanged();
    public OnCraftModChanged onCraftModChangedCallback;

    private PlayerInventory _inventory;

    void Start()
    {
        _inventory = PlayerInventory.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _inventory.SwitchBolt();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            onCraftModChangedCallback?.Invoke();
        }
    }
}
