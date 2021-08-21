using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private WeaponDefinition[] weaponDefinitions;

    private MoveVelocity moveVelocityScript;
    private float _currentHealth;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    public float health
    {
        get { return _currentHealth; }
        set 
        {
            if (value > 0)
            {
                _currentHealth = value;
            }
            else
            {
                Debug.LogWarning("Потрачено. Оформить похоронку.");
            }
        }
    }

    private void Awake()
    {
        WEAPON_DICT = new Dictionary<WeaponType, WeaponDefinition>();

        foreach (WeaponDefinition def in weaponDefinitions)
            WEAPON_DICT[def.type] = def;
    }

    private void Start()
    {
        moveVelocityScript = GetComponent<MoveVelocity>();

        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanFire())
        {
            fireDelegate();
        }
    }

    public bool CanFire()
    {
        bool canFire = moveVelocityScript.isRunning;
        return !canFire;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
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
