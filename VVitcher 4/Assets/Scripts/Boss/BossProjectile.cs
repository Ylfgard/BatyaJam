using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour {
    public float averageSpeed, minSpeedMultiplayer, maxSpeedMultiplayer, gravity, tilt, damage;

    float objGravity = 0;
    Vector3 direction;

    void Start()
    {
        averageSpeed = averageSpeed * Random.Range(minSpeedMultiplayer, maxSpeedMultiplayer);
    }

    void Update()
    {
        objGravity += gravity * Time.deltaTime;
        direction = -transform.forward;
        direction.y = objGravity;

        transform.Translate(direction * averageSpeed * Time.deltaTime, Space.World);
        transform.RotateAround(transform.position, transform.right, tilt * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMain>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
