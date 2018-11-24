using UnityEngine;

namespace Assets
{
    public class GameSystem : MonoBehaviour
    {

        public float speed = 5.0f;

        private GameSystem m_Instance;
        public GameSystem Instance { get { return m_Instance; } }

        void Awake()
        {
            m_Instance = this;
        }

        void OnDestroy()
        {
            m_Instance = null;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("space key was pressed");
            }
        }

        void OnGui()
        {
            // common GUI code goes here
        }

        // etc.
    }
}
