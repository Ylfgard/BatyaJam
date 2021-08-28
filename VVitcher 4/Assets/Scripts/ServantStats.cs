using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantStats : MonoBehaviour {
    public float startFollowDistance;

    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;

    private int _currentHealth;

    public float attackRange { get { return _attackRange; } }

    void Start()
    {
        _currentHealth = _health;
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("dead");
            return;
        }
        GetComponent<Animator>().SetTrigger("damage");
        GetComponent<Animator>().SetBool("isFollowing", true);
    }

    public int GetDamage()
    {
        return _damage;
    }
}
