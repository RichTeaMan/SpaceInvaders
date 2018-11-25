using UnityEngine;

namespace Assets
{
    public class PlayerShip : MonoBehaviour
    {
        public GameObject bulletPrefab;

        public float speed = 15.0f;

        public static PlayerShip Instance { get; private set; }

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
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }

        }

        private void Fire()
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = Instantiate(
                bulletPrefab,
                transform.position,
                transform.rotation);

            bullet.name = Constants.PLAYER_BULLET_NAME;

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.up * 20;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 20.0f);
        }
    }
}
