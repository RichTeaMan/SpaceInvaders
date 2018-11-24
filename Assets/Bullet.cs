using UnityEngine;

namespace Assets
{
    public class Bullet : MonoBehaviour
    {

        void OnCollisionEnter(Collision collision)
        {
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
