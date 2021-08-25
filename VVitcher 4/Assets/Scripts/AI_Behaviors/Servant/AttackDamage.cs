using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    private PlayerMain player;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMain>().GetComponent<PlayerMain>();
    }

    private void ServantAttack()
    {
        int damage = animator.GetComponent<ServantStats>().GetDamage();
        player.TakeDamage(damage);
    }
}
