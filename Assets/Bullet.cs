using UnityEngine;

namespace Assets
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            var bulletTarget = collision.gameObject.GetComponent<BulletTarget>();
            bulletTarget?.Hit(collision, gameObject);
            
        }
    }
}
