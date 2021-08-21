using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    simpleBolt,
    bloodyBolt,
    creakyBolt,
    linthyBolt
}

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.simpleBolt;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public bool emissive;
    public float velocity = 0;
    public float damageOnHit = 0;
    public float delayBetweenShots = 0;
}

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform projectileAnchor;

    private WeaponType _type = WeaponType.simpleBolt;
    private WeaponDefinition def;
    private float lastShotTime;


    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;
        def = PlayerMain.GetWeaponDefinition(_type);
        lastShotTime = Time.time;
    }

    private void Start()
    {
        SetType(_type);

        player.GetComponent<PlayerMain>().fireDelegate += Fire;
    }

    public void Fire()
    {
        // Если gameObject неактивен, выйти
        if (!gameObject.activeInHierarchy) return;
        // Если между выстрелами прошло недостаточно много времени, выйти
        if (Time.time - lastShotTime < def.delayBetweenShots) return;
        // Если нет выбранных болтов, выйти
        if (!PlayerInventory.instance.UseBolt(GetActiveBolt())) return;

        Projectile p;
        Vector3 vel = transform.forward * def.velocity;


        switch (type)
        {
            case WeaponType.simpleBolt:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;

            case WeaponType.bloodyBolt:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;

            case WeaponType.creakyBolt:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;

            case WeaponType.linthyBolt:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate(def.projectilePrefab, transform.position, transform.rotation);
        go.transform.SetParent(projectileAnchor, true);

        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShotTime = Time.time;
        return (p);
    }

    public WeaponType GetActiveBolt()
    {
        WeaponType boltActive = PlayerInventory.instance.GetActiveBoltType();

        //WeaponType wt = WeaponType.linthyBolt;
        type = boltActive;
        return boltActive;

        //Debug.LogWarning("Can't get bolt type from inventory.");
    }
}
