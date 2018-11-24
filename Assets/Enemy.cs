using UnityEngine;

namespace Assets
{
    public class Enemy : MonoBehaviour
    {
        private void Start()
        {
            EnemyAi.Instance.EnemySet.Add(this);
            
        }
        private void Awake()
        {
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == Constants.PLAYER_BULLET_NAME)
            {
                Destroy(this);
            }
        }

        private void OnDestroy()
        {
            EnemyAi.Instance.EnemySet.Remove(this);
        }
    }
}
