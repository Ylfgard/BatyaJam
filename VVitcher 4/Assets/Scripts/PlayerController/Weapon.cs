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
    private Transform aimTarger;
    [SerializeField]
    private Transform projectileAnchor;

    private Camera mainCam;
    private WeaponType _type = WeaponType.simpleBolt;
    private WeaponDefinition def;
    private float lastShotTime;
    private Vector3 dir;

    private PlayerAnimationStateController playerAnimationStateControllerScript;
    private PlayerMain playerMainScript;

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
        mainCam = Camera.main;
        playerAnimationStateControllerScript = player.GetComponent<PlayerAnimationStateController>();

        SetType(_type);

        playerMainScript = player.GetComponent<PlayerMain>();
        playerMainScript.fireDelegate += Fire;
    }

    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Time.time - lastShotTime < def.delayBetweenShots) return;
        if (!PlayerInventory.instance.UseBolt(GetActiveBolt())) return;

        Projectile p;

        //directionToMouse = (mouseTarget.transform.position - transform.position).normalized;
        dir = (aimTarger.position - mainCam.transform.position).normalized;
        //dir = new Vector3(dir.x, 0, dir.z);

        Vector3 vel = dir * def.velocity;


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

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/reload_crossbow");
        playerAnimationStateControllerScript.PlayFiringAnim();
    }

    public Projectile MakeProjectile()
    {
        //Vector3 directionToMouse = (mouseTarget.transform.position - transform.position).normalized;
        //directionToMouse = new Vector3(directionToMouse.x, 0, directionToMouse.z);

        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/shot_crossbow");
        GameObject go = Instantiate(def.projectilePrefab, transform.position, Quaternion.LookRotation(dir));
        go.transform.SetParent(projectileAnchor, true);

        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShotTime = Time.time;
        playerMainScript.reloadTime = def.delayBetweenShots;
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
