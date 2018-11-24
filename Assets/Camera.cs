using UnityEngine;

namespace Assets
{
    public class Camera : MonoBehaviour
    {
        public GameObject bulletPrefab;

        public float speed = 5.0f;

        private static Camera m_Instance;
        public static Camera Instance { get { return m_Instance; } }

        private void Awake()
        {
            bulletPrefab = GameObject.Find("Bullet");
            m_Instance = this;
        }

        private void OnDestroy()
        {
            m_Instance = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("space key was pressed");
                Fire();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
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
