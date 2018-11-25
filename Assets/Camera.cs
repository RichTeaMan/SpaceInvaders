﻿using UnityEngine;

namespace Assets
{
    public class Camera : MonoBehaviour
    {
        public GameObject bulletPrefab;

        public float speed = 15.0f;

        public float speedH = 2.0f;
        public float speedV = 2.0f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;

        private bool cameraArcadeMode = false;

        public static Camera Instance { get; private set; }

        private void Awake()
        {
            bulletPrefab = (GameObject)Resources.Load("Models/Bullet", typeof(GameObject));
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("space key was pressed");
                Fire();
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.Z))
            {
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.X))
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (cameraArcadeMode)
                {
                    transform.position = new Vector3(0, 0, 0);
                    transform.rotation = Quaternion.Euler(270, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(0, 30, -50);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                cameraArcadeMode = !cameraArcadeMode;
            }


            if (Input.GetKey(KeyCode.Q))
            {
                yaw += speedH * Input.GetAxis("Mouse X");
                pitch -= speedV * Input.GetAxis("Mouse Y");

                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
        }

        private void OnGui()
        {
            // common GUI code goes here
        }

        // etc.

        private void Fire()
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = Instantiate(
                bulletPrefab,
                transform.position,
                transform.rotation);

            bullet.name = Constants.PLAYER_BULLET_NAME;

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 20.0f);
        }
    }
}
