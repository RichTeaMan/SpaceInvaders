using UnityEngine;

namespace Assets
{
    public class Enemy : MonoBehaviour
    {

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == Constants.PLAYER_BULLET_NAME)
            {
                Destroy(this);
            }
        }
    }
}
