using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchdemonStats : MonoBehaviour {
    public delegate void OnBossDead();
    public OnBossDead onBossDeadCallback;

    public delegate void OnBossHealthChanged(int health);
    public OnBossHealthChanged onBossHealthChangedCallback;

    public FanProjectiles fanProjectiles;
    public int fanCastNumber;

    [SerializeField] private int _startHealth;

    private int _health;
    private float _offsetProjectiles = 5f;

    void Start()
    {
        _health = _startHealth;

        GetComponent<Animator>().SetInteger("maxHealth", _startHealth);
        GetComponent<Animator>().SetInteger("health", _startHealth);
    }

    public void Damage(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.simpleBolt:
                {
                    _health -= 2;
                    break;
                }
            case WeaponType.bloodyBolt:
                {
                    _health -= 10;
                    break;
                }
            case WeaponType.creakyBolt:
                {
                    break;
                }
            case WeaponType.linthyBolt:
                {
                    if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    {
                        GetComponent<Animator>().SetTrigger("stun");
                    }
                    break;
                }
        }

        GetComponent<Animator>().SetInteger("health", _health);

        if (_health <= 0)
        {
            onBossDeadCallback?.Invoke();
            GetComponent<Animator>().SetTrigger("death");
            return;
        }
        onBossHealthChangedCallback?.Invoke(_health);
    }

    public void Attack()
    {
        StartCoroutine(AttackPatternFan());
    }

    private IEnumerator AttackPatternFan()
    {
        GetComponent<FMODUnity.StudioEventEmitter>().Play();

        Quaternion offsetRotation = Quaternion.AngleAxis(0, transform.up);

        for (int index = 0; index < fanCastNumber; index++)
        {
            offsetRotation = Quaternion.AngleAxis(index * _offsetProjectiles, transform.up);
            FanProjectiles fan = Instantiate(fanProjectiles, transform.position + Vector3.up, transform.rotation);
            fan.SpawnProjectiles(offsetRotation);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
