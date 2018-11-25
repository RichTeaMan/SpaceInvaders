using UnityEngine;

namespace Assets
{
    public class Control : MonoBehaviour
    {
        public float speed = 15.0f;

        public float speedH = 2.0f;
        public float speedV = 2.0f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;

        private bool cameraArcadeMode = false;

        private Vector3 offset = new Vector3(0, -0.03f, 0);

        public static Control Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.Z))
            {
                transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.X))
            {
                transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (cameraArcadeMode)
                {
                    // switch to under aliens
                    Camera.main.fieldOfView = 30.0f;
                    transform.position = new Vector3(0, -0.03f, 0);
                    transform.rotation = Quaternion.Euler(270, 0, 0);
                }
                else
                {
                    // switch to arcade
                    Camera.main.fieldOfView = 60.0f;
                    transform.position = new Vector3(0, 30, -50);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                cameraArcadeMode = !cameraArcadeMode;
            }


            if (Input.GetKey(KeyCode.Q))
            {
                yaw += speedH * Input.GetAxis("Mouse X");
                pitch -= speedV * Input.GetAxis("Mouse Y");

                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

        }

        private void LateUpdate()
        {
            if (!cameraArcadeMode)
            {
                // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
                transform.position = PlayerShip.Instance.transform.position + offset;
            }
        }

        private void OnGui()
        {
            // common GUI code goes here
        }

    }
}
