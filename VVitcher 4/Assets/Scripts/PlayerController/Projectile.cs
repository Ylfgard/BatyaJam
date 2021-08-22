using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private string enemyTag = "Enemy";
    [SerializeField]
    private float projectileLifetime = 3f;

    private Rigidbody _rb;
    private Renderer _rend;
    private WeaponType _type;
    private float lifetime;

    private int _damageToServant = 2;

    public WeaponType type
    {
        get
        { return (_type); }
        set
        { SetType(value); }
    }

    public Rigidbody rb
    {
        get { return _rb; }
        private set { _rb = value; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rend = GetComponentInChildren<Renderer>();
    }

    private void Start()
    {
        lifetime = Time.time + projectileLifetime;
    }

    private void Update()
    {
        if (lifetime < Time.time)
        {
            ProjectileDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileDestroy();

        if (other.gameObject.CompareTag(enemyTag))
        {
            ServantStats servant = other.GetComponent<ServantStats>();
            if(servant != null)
            {
                servant.Damage(_damageToServant);
            }

            ArchdemonStats archedemon = other.GetComponent<ArchdemonStats>();
            if (archedemon != null)
            {
                archedemon.Damage(type);
            }

            Debug.LogWarning("Projectile hits Enemy. Do something...");
        }
        else
        {
            Debug.Log("Projectile hits obstacle: " + other.name);
        }
    }

    public void SetType(WeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = PlayerMain.GetWeaponDefinition(_type);
        _rend.material.color = def.projectileColor;
        if (def.emissive)
        {
            _rend.material.SetColor("_EmissionColor", def.projectileColor);
            _rend.material.EnableKeyword("_EMISSION");
        }
        Debug.Log(def.type);
    }

    private void ProjectileDestroy()
    {
        Destroy(gameObject);
    }
}
