using UnityEngine;

namespace Assets
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            Destroy(collision.gameObject);
            Destroy(this);
        }
    }
}
