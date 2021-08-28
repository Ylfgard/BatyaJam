using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereProjectiles : MonoBehaviour {
    public GameObject bossProjectile;

    [Range(1, 100)] public int projectilesAmount;
    public float spawnRadius;
    public float selfDestroyTimer;

    private List<Transform> _projectiles = new List<Transform>();
    private int _cutoutOffset = 6;

    void Start()
    {
        //int rnd = Random.Range(5, 36);
        //SpawnProjectiles(Quaternion.AngleAxis(0, transform.up), rnd);
    }

    private void Update()
    {
        if (selfDestroyTimer > 0)
        {
            selfDestroyTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnProjectiles(Quaternion offsetRotation, int cutoutPos)
    {
        if (projectilesAmount > _projectiles.Count)
        {
            for (int projectileIndex = _projectiles.Count; projectileIndex < projectilesAmount; projectileIndex++)
            {
                GameObject projectile = Instantiate(bossProjectile, Vector3.zero, Quaternion.identity);
                projectile.transform.SetParent(transform);
                _projectiles.Add(projectile.transform);
            }
        }

        Quaternion quaternion = Quaternion.AngleAxis(200f / (float)projectilesAmount, transform.up);
        Vector3 vect3 = offsetRotation * Quaternion.Euler(0, -90, 0) * transform.forward * spawnRadius;
        for (int index = 0; index < projectilesAmount; index++)
        {
            _projectiles[index].position = transform.position + vect3;
            _projectiles[index].LookAt(transform);
            if (index>cutoutPos && index < cutoutPos+_cutoutOffset)
            {
                Destroy(_projectiles[index].gameObject);
            }
            vect3 = quaternion * vect3;
        }
    }
}
