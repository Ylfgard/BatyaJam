using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantAnimEvents : MonoBehaviour
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

    private void ServantFalls()
    {
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false; ;
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.down * 1.5f;
        StartCoroutine(ServantDestroy());
    }

    IEnumerator ServantDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
