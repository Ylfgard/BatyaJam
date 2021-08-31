using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchdemonStats : MonoBehaviour {
    public delegate void OnBossActive();
    public OnBossActive onBossActiveCallback;
    
    public delegate void OnBossDead();
    public OnBossDead onBossDeadCallback;

    public delegate void OnBossHealthChanged(int health);
    public OnBossHealthChanged onBossHealthChangedCallback;

    public FanProjectiles fanProjectiles;
    public int fanCastNumber;

    public BeamAttack beamAttack;
    public SphereProjectiles sphereProjectiles;

    [SerializeField] public int startHealth;

    public int health;
    private float _offsetProjectiles = 5f;

    private FMOD.Studio.EventInstance instance;

    void Start()
    {
        health = startHealth;

        GetComponent<Animator>().SetInteger("maxHealth", startHealth);
        GetComponent<Animator>().SetInteger("health", startHealth);
    }

    public void Damage(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.simpleBolt:
                {
                    health -= 2;
                    break;
                }
            case WeaponType.bloodyBolt:
                {
                    health -= 10;
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

        GetComponent<Animator>().SetInteger("health", health);

        if (health <= 0)
        {
            onBossDeadCallback?.Invoke();
            GetComponent<Animator>().SetTrigger("death");
            return;
        }
        onBossHealthChangedCallback?.Invoke(health);
    }

    public void Attack1()
    {
        StartCoroutine(AttackPatternFan());
    }
    public void Attak1Sound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/demon_attack_1");
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
    }

    public void Attack2()
    {
        Instantiate(beamAttack, transform.position + Vector3.up, transform.rotation);
    }
    public void Attak2Sound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/demon_attack_2");
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
    }

    public void Attack3()
    {
        StartCoroutine(AttackPatternSphere());
    }
    public void Attak3Sound()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/demon_attack_3");
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        instance.start();
    }

    private IEnumerator AttackPatternFan()
    {
        //GetComponent<FMODUnity.StudioEventEmitter>().Play();

        Quaternion offsetRotation = Quaternion.AngleAxis(0, transform.up);

        for (int index = 0; index < fanCastNumber; index++)
        {
            offsetRotation = Quaternion.AngleAxis(index * _offsetProjectiles, transform.up);
            FanProjectiles fan = Instantiate(fanProjectiles, transform.position + Vector3.up, transform.rotation);
            fan.SpawnProjectiles(offsetRotation);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator AttackPatternSphere()
    {
        Quaternion offsetRotation = Quaternion.AngleAxis(0, transform.up);
        int rnd = Random.Range(15, 20);

        for (int index = 0; index < fanCastNumber; index++)
        {
            SphereProjectiles sphere = Instantiate(sphereProjectiles, transform.position + Vector3.up, transform.rotation);
            sphere.SpawnProjectiles(offsetRotation, rnd);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnEnable()
    {
        onBossActiveCallback?.Invoke();
    }
}
