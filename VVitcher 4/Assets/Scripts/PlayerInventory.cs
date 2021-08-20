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

    public delegate void OnHerbInventoryChanged();
    public OnHerbInventoryChanged onHerbInventoryChangedCallback;

    public delegate void OnBoltInventoryChanged();
    public OnBoltInventoryChanged onBoltInventoryChangedCallback;

    public delegate void OnBoltChanged(int activeBoltIndex);
    public OnBoltChanged onBoltChangedCallback;

    private int[] _herbsCount;
    private int[] _boltsCount;
    private int _activeBoltIndex;

    void Start()
    {
        _herbsCount = new int[Enum.GetNames(typeof(HerbType)).Length];
        _boltsCount = new int[Enum.GetNames(typeof(HerbType)).Length];

        _activeBoltIndex = 0;
    }

    public void AddHerb(HerbType type)
    {
        _herbsCount[(int)type]++;
        onHerbInventoryChangedCallback?.Invoke();
    }

    public int[] GetHerbs()
    {
        return _herbsCount;
    }

    public void AddBolt(HerbType type)
    {
        _boltsCount[(int)type]++;
        onHerbInventoryChangedCallback?.Invoke();
    }

    public int[] GetBolts()
    {
        return _boltsCount;
    }

    public void SwitchBolt()
    {
        onBoltChangedCallback?.Invoke(_activeBoltIndex);

        if (_activeBoltIndex < _herbsCount.Length) _activeBoltIndex++;
        else _activeBoltIndex = 0;
    }

    public void CraftBolts(int typeIndex)
    {
        if (_herbsCount[typeIndex] <= 0) return;

        _herbsCount[typeIndex]--;
        _boltsCount[typeIndex]++;
        onBoltInventoryChangedCallback?.Invoke();
        onHerbInventoryChangedCallback?.Invoke();
    }
}
