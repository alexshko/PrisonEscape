using alexshko.prisonescape.Shooting;
using System;
using UnityEngine;

namespace alexshko.prisonescape.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        public Vector2 mouseSpeed;
        public float MouseAimMaxAngle = 90;
        public float MouseAimMinAngle = -90;
        //sum the change in the mouseAim:
        private float vertMouse;
        private float horzMouse;

        private Transform cam;
        private RifleMechanism rifleRef;

        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main.transform;
            vertMouse = cam.localEulerAngles.y;
            horzMouse = cam.localEulerAngles.x;

            rifleRef = GetComponentInChildren<RifleMechanism>();
            if (rifleRef == null)
            {
                Debug.LogError("Missing Rifle Component");
            }
        }

        // Update is called once per frame
        void Update()
        {
            MakeMouseMove();
            if (Input.GetButton("Fire1"))
            {
                FireWaepon();
            }
        }

        private void FireWaepon()
        {
            rifleRef.Fire();
        }

        private void MakeMouseMove()
        {
            vertMouse += Input.GetAxis("Mouse X") * mouseSpeed.x;
            horzMouse += Input.GetAxis("Mouse Y") * mouseSpeed.y;

            //make sure the new angle is in the clamp limit:
            vertMouse = Mathf.Clamp(vertMouse, MouseAimMinAngle, MouseAimMaxAngle);
            horzMouse = Mathf.Clamp(horzMouse, MouseAimMinAngle, MouseAimMaxAngle);

            Debug.Log("vert:" + vertMouse);
            cam.localEulerAngles = new Vector3(-horzMouse, vertMouse, 0);
        }
    }
}
