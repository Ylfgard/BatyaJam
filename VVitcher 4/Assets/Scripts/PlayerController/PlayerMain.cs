using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public delegate void OnPlayerDead();
    public OnPlayerDead onPlayerDeadCallback;

    public delegate void OnHealthChanged(int health);
    public OnHealthChanged onHealthChangedCallback;

    public static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private WeaponDefinition[] weaponDefinitions;

    private PlayerMain playerMainScript;
    private MoveVelocity moveVelocityScript;
    private PlayerAnimationStateController playerAnimationStateControllerScript;
    private float _currentHealth = 60f;
    private bool _canFire;
    private bool _isDead;

    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    public bool isDead { get { return _isDead; } }
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
                _isDead = true;
                playerAnimationStateControllerScript.PlayDyingAnim();

                onPlayerDeadCallback?.Invoke();
            }

            onHealthChangedCallback?.Invoke((int)health);
        }
    }
    public bool isMaxHealth { get; private set; }
    public bool canFire
    {
        get
        {
            if (isDead) return false;

            if (moveVelocityScript.isRunning || !playerAnimationStateControllerScript.isAgressive)
                _canFire = false;
            else
                _canFire = true;

            return _canFire;
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
        playerAnimationStateControllerScript = GetComponent<PlayerAnimationStateController>();

        health = maxHealth;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            fireDelegate();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(15);
        }
    }

    public void AbsorbHealingPowerup(int hp)
    {
        health += hp;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (isMaxHealth) isMaxHealth = false;

        playerAnimationStateControllerScript.PlayHitReactionAnim();
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if (WEAPON_DICT.ContainsKey(wt))
        {
            return (WEAPON_DICT[wt]);
        }
        
        return (new WeaponDefinition());
    }

    public int GetMaxHealth()
    {
        return (int)maxHealth;
    }
}
