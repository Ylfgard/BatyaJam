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
    [Header("Input tags:")]
    [SerializeField]
    private string powerupHPTag;

    private MoveVelocity moveVelocityScript;
    private GameObject lastTriggeredGameobject;
    private float _currentHealth = 60f;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    public bool isMaxHealth { get; private set; }

    public float health
    {
        get { return _currentHealth; }
        set 
        {
            if (value > 0)
            {
                _currentHealth = value;
                if (_currentHealth >= maxHealth)
                {
                    _currentHealth = maxHealth;
                    isMaxHealth = true;
                }
            }
            else
            {
                Debug.LogError("Потрачено. GAME OVER!");
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

        //health = maxHealth;
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

    public void AbsorbHealingPowerup(int hp)
    {
        Debug.Log(health + " + " + hp);
        health += hp;
        Debug.Log(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (isMaxHealth) isMaxHealth = false;
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
