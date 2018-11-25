using UnityEngine;

namespace Assets
{
    public class PlayerShip : MonoBehaviour, BulletTarget
    {
        public GameObject bulletPrefab;

        public float speed = 15.0f;

        public static PlayerShip Instance { get; private set; }

        public int Hits { get; private set; }

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
                transform.position + new Vector3(0, 3.8f, 0),
                transform.rotation);

            // -2.1f is an adjustment so enemies aren't killed by their own bullet. This value was found with trial
            // and error. Perhaps a better way can be found, perhaps with these:
            // var collider = GetComponent<Collider>();
            // var size = GetComponent<Collider>().bounds.size;

            bullet.name = Constants.PLAYER_BULLET_NAME;

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 20.0f);
        }

        public void Hit(Collision collision, GameObject bullet)
        {
            Destroy(bullet);
            Hits++;
        }
    }
}
