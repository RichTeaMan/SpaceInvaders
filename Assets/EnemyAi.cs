using UnityEngine;

namespace Assets
{
    public class EnemyAi : MonoBehaviour
    {

        public float speed = 5.0f;

        public int StartingAliens;

        public bool HasStarted = false;

        private void Awake()
        {
            StartingAliens = 120;
        }

        // Use this for initialization
        private void Start()
        {
            if (HasStarted)
                return;

            HasStarted = true;

            var basicEnemyPrefab = GameObject.Find("BasicEnemy");

            float startX = -30.0f;
            float incrementX = 5.0f;

            float currentX = startX;

            for (int i = 0; i < StartingAliens; i++)
            {
                var startPosition = new Vector3(currentX, 2.0f, 20.0f);

                var enemy = Instantiate(
                    basicEnemyPrefab,
                    startPosition,
                    Quaternion.identity);

                enemy.name = string.Format("Enemy_{0}", i);

                currentX += incrementX;
            }
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}
