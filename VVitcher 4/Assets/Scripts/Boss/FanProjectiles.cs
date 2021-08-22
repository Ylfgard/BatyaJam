using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanProjectiles : MonoBehaviour {
    public GameObject bossProjectile;

    [Range(1, 100)] public int projectilesAmount;
    public float spawnRadius;
    public float selfDestroyTimer;

    List<Transform> projectiles = new List<Transform>();

    void Start()
    {
        //SpawnProjectiles();
    }

    private void Update()
    {
        if(selfDestroyTimer > 0)
        {
            selfDestroyTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnProjectiles(Quaternion offsetRotation)
    {
        if (projectilesAmount > projectiles.Count)
        {
            for (int projectileIndex = projectiles.Count; projectileIndex < projectilesAmount; projectileIndex++)
            {
                GameObject projectile = Instantiate(bossProjectile, Vector3.zero, Quaternion.identity);
                projectile.transform.SetParent(transform);
                projectiles.Add(projectile.transform);
            }
        }

        Quaternion quaternion = Quaternion.AngleAxis(200f / (float)projectilesAmount, transform.up);
        //Quaternion offsetRotation = Quaternion.AngleAxis(Random.Range(-20f,20f), transform.up);
        Vector3 vect3 = offsetRotation * Quaternion.Euler(0, -90, 0) * transform.forward * spawnRadius;
        for (int index = 0; index < projectilesAmount; index++)
        {
            projectiles[index].position = transform.position + vect3;
            projectiles[index].LookAt(transform);
            vect3 = quaternion * vect3;
        }
    }
}
