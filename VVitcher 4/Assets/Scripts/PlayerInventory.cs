using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    #region Singleton
    public static PlayerInventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerInventory found!");
        }

        instance = this;
    }
    #endregion

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    private int[] _herbsCount;

    void Start()
    {
        _herbsCount = new int[Enum.GetNames(typeof(HerbType)).Length];
    }

    public void AddHerb(HerbType type)
    {
        _herbsCount[(int)type]++;
        onInventoryChangedCallback?.Invoke();
    }

    public int[] GetHerbs()
    {
        return _herbsCount;
    }
}
