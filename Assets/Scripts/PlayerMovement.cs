using alexshko.prisonescape.Shooting;
using UnityEngine;

namespace alexshko.prisonescape.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [Tooltip("Movement speed")]
        public float playerSpeed = 10;
        [Tooltip("Mouse movement speed")]
        public Vector2 mouseSpeed;
        [Tooltip("Max. angle of mouse movement.")]
        public float MouseAimMaxAngle = 90;
        [Tooltip("Min. angle of mouse movement.")]
        public float MouseAimMinAngle = -90;
        //sum the change in the mouseAim angle:
        private float vertMouse;
        private float horzMouse;

        private CharacterController character;
        //main camera is Player's child:
        private Transform cam;
        private RifleMechanism rifleRef;

        //sum the move to make in the current frame
        private Vector3 moveToMake;

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

            character = GetComponent<CharacterController>();
            if (character == null)
            {
                Debug.LogError("Missing character controller");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Core.GameController.Instance.isGamePlaying)
            {
                MakeMouseMove();
                MakeMove();
            }
        }

        private void MakeMouseMove()
        {
            vertMouse += Input.GetAxis("Mouse X") * mouseSpeed.x;
            horzMouse += Input.GetAxis("Mouse Y") * mouseSpeed.y;

            //make sure the new angle is in the clamp limit:
            vertMouse = Mathf.Clamp(vertMouse, MouseAimMinAngle, MouseAimMaxAngle);
            horzMouse = Mathf.Clamp(horzMouse, MouseAimMinAngle, MouseAimMaxAngle);

            Debug.Log("vert:" + vertMouse);
            //make the character turn with the mouse movement:
            character.transform.localEulerAngles = new Vector3(-horzMouse, vertMouse, 0);
        }
    
        //calculate only the next move
        private void MakeMove()
        {
            moveToMake = Vector3.zero;

            float moveZ = Input.GetAxis("Vertical");
            float moveX = Input.GetAxis("Horizontal");
            moveToMake = moveZ * transform.forward + moveX * transform.right;
            //the direction has to be normalized first, then applied by speed and deltaTime for animation
            moveToMake = moveToMake.normalized * playerSpeed * Time.deltaTime;
        }

        //make the move calculated in Update. for animation smoothness purpose:
        private void FixedUpdate()
        {
            if (Core.GameController.Instance.isGamePlaying)
            {
                character.SimpleMove(moveToMake);
            }
        }
    }
}
