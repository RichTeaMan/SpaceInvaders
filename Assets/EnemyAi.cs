using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    public class EnemyAi : MonoBehaviour
    {
        public static EnemyAi Instance { get; private set; }

        public HashSet<Enemy> EnemySet = new HashSet<Enemy>();

        public float speed = 5.0f;

        public float LeftCut = -40.0f;

        public float RightCut = 40.0f;

        public float EnemySpeed = 2.0f;

        public int AlienColumnWidth = 12;

        public float IncrementX = 5.0f;

        public float IncrementY = 5.0f;

        public int AlienRowHeight = 5;

        /// <summary>
        /// The distance an alien 'row' should drop when getting closer.
        /// </summary>
        public float DropDistance = 0.5f;

        public float DropSpeed = 10.0f;

        private float DropTarget = float.MaxValue;

        private bool DroppingAliens = false;

        public bool HasStarted = false;

        private void Awake()
        {
            Instance = this;
        }

        // Use this for initialization
        private void Start()
        {
            if (HasStarted)
                return;

            HasStarted = true;

            var basicEnemyPrefab = (GameObject)Resources.Load("Models/BasicEnemy", typeof(GameObject));

            float startX = -30.0f;
            float startY = 40.0f;
            float currentY = startY;

            for (int y = 0; y < AlienRowHeight; y++)
            {
                float currentX = startX;
                for (int i = 0; i < AlienColumnWidth; i++)
                {
                    var startPosition = new Vector3(currentX, 2.0f, currentY);

                    var enemyModel = Instantiate(
                        basicEnemyPrefab,
                        startPosition,
                        Quaternion.Euler(0, 180, 0));

                    enemyModel.name = string.Format("Enemy_{0}", i);

                    enemyModel.GetComponent<Rigidbody>().velocity = enemyModel.transform.right * -EnemySpeed;

                    currentX += IncrementX;

                    var enemy = enemyModel.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.EnemyCoordinate = new EnemyCoordinate { X = i, Y = y };
                    }
                }
                currentY -= IncrementY;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (!EnemySet.Any())
                return;

            // find most left
            var leftPos = EnemySet.Min(e => e.transform.position.x);
            // find most right
            var rightPos = EnemySet.Max(e => e.transform.position.x);
            // find most down
            var downPos = EnemySet.Min(e => e.transform.position.z);
            if (DroppingAliens)
            {
                if (downPos < DropTarget)
                {
                    DroppingAliens = false;
                    float speed = 0.0f;
                    if (leftPos < LeftCut + 0.5f)
                    {
                        speed = -EnemySpeed;
                    }
                    else if (rightPos > RightCut - 0.5f)
                    {
                        speed = EnemySpeed;
                    }
                    foreach (var enemy in EnemySet)
                    {
                        enemy.GetComponent<Rigidbody>().velocity = enemy.transform.right * speed;
                    }
                }
                else
                {
                    foreach (var enemy in EnemySet)
                    {
                        enemy.GetComponent<Rigidbody>().velocity = enemy.transform.forward * DropSpeed;
                    }
                }
            }
            else if (leftPos < LeftCut || rightPos > RightCut)
            {
                DroppingAliens = true;
                DropTarget = downPos - DropDistance;
            }

            if (Time.frameCount % 40 == 0)
            {
                // only the closest enemy should fire
                foreach (var enemyColumn in EnemySet.GroupBy(e => e.EnemyCoordinate.X))
                {
                    var closest = enemyColumn.OrderByDescending(e => e.EnemyCoordinate.Y).First();
                    closest.Fire();
                }
                
            }
        }
    }
}
