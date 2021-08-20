using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    private PlayerInventory _inventory;

    void Start()
    {
        _inventory = PlayerInventory.instance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _inventory.SwitchBolt();
        }
    }
}
