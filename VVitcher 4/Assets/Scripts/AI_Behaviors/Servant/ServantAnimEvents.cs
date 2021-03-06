using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServantAnimEvents : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    private PlayerMain player;
    private Animator animator;
    private float _attackRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMain>().GetComponent<PlayerMain>();

        _attackRange = animator.GetComponent<ServantStats>().attackRange;
    }

    private void ServantAttack()
    {
        if (Vector3.Distance(animator.transform.position, player.transform.position) < _attackRange)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/damage_from_servants");
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            instance.start();
            //FMODUnity.RuntimeManager.CreateInstance("event:/SFX/damage_from_servants");
            int damage = animator.GetComponent<ServantStats>().GetDamage();
            player.TakeDamage(damage);
        }
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
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }
}
