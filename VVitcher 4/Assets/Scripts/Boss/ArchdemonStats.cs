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
    private float _offsetProjectiles = 2f;

    void Start()
    {
        _health = _startHealth;

        GetComponent<Animator>().SetInteger("maxHealth", _startHealth);
        GetComponent<Animator>().SetInteger("health", _startHealth);
    }

    public void Damage(int damage)
    {
        _health -= damage;

        GetComponent<Animator>().SetInteger("health", _health);

        if (_health <= 0)
        {
            onBossDeadCallback?.Invoke();
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
