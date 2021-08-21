using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    simple,
    bloody,
    creaky,
    linthy
}

[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.simple;
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
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
    [SerializeField]
    //private string projectilePlayerTag = "Projectile_player";
    //[SerializeField]
    //private LayerMask projectilePlayerLayerMask;

    private WeaponType _type = WeaponType.simple;
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
        // Если this.gameObject неактивен, выйти
        if (!gameObject.activeInHierarchy) return;
        // Если между выстрелами прошло недостаточно много времени, выйти
        if (Time.time - lastShotTime < def.delayBetweenShots) return;

        Projectile p;
        Vector3 vel = transform.forward * def.velocity;

        //if (transform.forward.z < 0)
        //{
        //    vel.z = -vel.z;
        //}

        switch (type)
        {
            case WeaponType.simple:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;
            case WeaponType.bloody:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;

            case WeaponType.creaky:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;

            case WeaponType.linthy:
                p = MakeProjectile();
                p.rb.velocity = vel;
                break;
        }

        Projectile MakeProjectile()
        {
            GameObject go = Instantiate(def.projectilePrefab, transform.position, transform.rotation);
            //go.tag = projectilePlayerTag;
            //go.layer = projectilePlayerLayerMask;

            //go.transform.position = transform.position;
            go.transform.SetParent(projectileAnchor, true);

            Projectile p = go.GetComponent<Projectile>();
            p.type = type;
            lastShotTime = Time.time;
            return (p);
        }
    }
}
