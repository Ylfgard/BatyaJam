using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public delegate void OnPlayerDead();
    public OnPlayerDead onPlayerDeadCallback;

    public delegate void OnHealthChanged(int health);
    public OnHealthChanged onHealthChangedCallback;
    
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;

    public static Dictionary<WeaponType, WeaponDefinition> WEAPON_DICT;

    [SerializeField]
    private bool GODMode;
    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float delayFireAndAnim = 0.2f;
    [SerializeField]
    private WeaponDefinition[] weaponDefinitions;

    private FMOD.Studio.EventInstance instance;
    private MoveVelocity moveVelocityScript;
    private CameraModeChanger cameraModeChangerScript;
    private PlayerAnimationStateController playerAnimationStateControllerScript;
    private float _currentHealth = 60f;
    private float _reloadingTimer;
    private bool _canFire;
    private bool _isFiring;
    private bool _isDead;

    public float reloadTime { get; set; }
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
                _currentHealth = 0;
                cameraModeChangerScript.ChangeOnDied();
                playerAnimationStateControllerScript.PlayDyingAnim();
                GetComponent<Rigidbody>().Sleep();

                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance = FMODUnity.RuntimeManager.CreateInstance("event:/UI/death_gg");
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                instance.start();

                onPlayerDeadCallback?.Invoke();
            }

            //Debug.Log((int)health);
            onHealthChangedCallback?.Invoke((int)health);
        }
    }
    public bool isMaxHealth { get; private set; }
    public bool canFire
    {
        get
        {
            if (isDead) return false;

            if (GamePauser.isGamePaused || _isFiring || IsReloading() || moveVelocityScript.isRunning || !playerAnimationStateControllerScript.isAgressive)
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
        //SetCameraAspect();

        moveVelocityScript = GetComponent<MoveVelocity>();
        cameraModeChangerScript = GetComponent<CameraModeChanger>();
        playerAnimationStateControllerScript = GetComponent<PlayerAnimationStateController>();

        health = maxHealth;
    }

    private void SetCameraAspect()
    {
        float targetAspect = 16.0f / 9.0f;
        float currentAspect = Screen.width / Screen.height;

        float scaleHeight = currentAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            _isFiring = true;
            StartCoroutine(AimThenFire());
        }
    }

    IEnumerator AimThenFire()
    {
        playerAnimationStateControllerScript.PlayFiringAnim();
        yield return new WaitForSeconds(delayFireAndAnim);
        fireDelegate();
        _isFiring = false;

        InitReloading();
    }

    public void InitReloading()
    {
        _reloadingTimer = Time.time + reloadTime;
    }

    private bool IsReloading()
    {
        return _reloadingTimer > Time.time;
    }

    public void AbsorbHealingPowerup(int hp)
    {
        health += hp;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        if(!GODMode) health -= damage;
        playerAnimationStateControllerScript.PlayHitReactionAnim();
        
        if (isMaxHealth) isMaxHealth = false;
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if (WEAPON_DICT.ContainsKey(wt))
            return (WEAPON_DICT[wt]);
        
        return (new WeaponDefinition());
    }

    public int GetMaxHealth()
    {
        return (int)maxHealth;
    }
}
