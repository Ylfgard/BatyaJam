using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    [SerializeField]
    private WeaponDefinition[] weaponDefinitions;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    private void Awake()
    {
        WEAPON_DICT = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAPON_DICT[def.type] = def;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fireDelegate();
        }
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if (WEAPON_DICT.ContainsKey(wt))
        {
            return (WEAPON_DICT[wt]);
        }
        
        return (new WeaponDefinition());
    }

}
