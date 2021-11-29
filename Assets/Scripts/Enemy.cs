using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private const float Cooldown = 1f;
    public Bullet Bullet;
    public float Health { get; set; } = 5f;

    private Player target;
    private float timeSinceLastShot;

    void Start()
    {
        
    }

    void Update()
    {
        if (target == null) {
            return;
        }

        transform.rotation = Math.LookAt2D(transform.position, target.transform.position);

        if (timeSinceLastShot >= Cooldown) {    // TODO Move to weapon class?
            var bullet = Instantiate(Bullet);
            bullet.Initialize(transform.position, transform.up, false);
            bullet.transform.rotation = transform.rotation;
            timeSinceLastShot = 0f;

            FindObjectOfType<SoundManager>().PlaySound("EnemyShot", true);
        }

        timeSinceLastShot += Time.deltaTime;
    }

    public void SetTarget(Player newTarget)
    {
        target = newTarget;
    }

    public void GetDamaged(float damage)
    {
        Health -= damage;
        if (Health <= 0) {
            Destroy(gameObject);    // TODO Do not die, just become disabled
        }
    }
}
