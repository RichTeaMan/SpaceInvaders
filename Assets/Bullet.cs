using UnityEngine;

namespace Assets
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            var bulletTarget = collider.GetComponent<BulletTarget>();
            bulletTarget?.Hit(collider, gameObject);
        }
    }
}
