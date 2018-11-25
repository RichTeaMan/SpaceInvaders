﻿using UnityEngine;

namespace Assets
{
    public class Enemy : MonoBehaviour, BulletTarget
    {
        public EnemyCoordinate EnemyCoordinate { get; set; }

        private GameObject bulletPrefab;
        private void Start()
        {
            bulletPrefab = (GameObject)Resources.Load("Models/Bullet", typeof(GameObject));
            EnemyAi.Instance.EnemySet.Add(this);
            
        }
        private void Awake()
        {
        }

        private void OnDestroy()
        {
            EnemyAi.Instance.EnemySet.Remove(this);
        }

        public void Fire()
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = Instantiate(
                bulletPrefab,
                transform.position + new Vector3(0, -2.1f, 0),
                transform.rotation);

            // -2.1f is an adjustment so enemies aren't killed by their own bullet. This value was found with trial
            // and error. Perhaps a better way can be found, perhaps with these:
            // var collider = GetComponent<Collider>();
            // var size = GetComponent<Collider>().bounds.size;

            bullet.name = Constants.ENEMY_BULLET_NAME;

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 20.0f);
        }

        public void Hit(Collider collider, GameObject bullet)
        {
            Destroy(bullet);
            Destroy(collider.gameObject);
        }
    }
}
