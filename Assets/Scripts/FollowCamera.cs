using UnityEngine;

namespace alexshko.prisonescape.Core
{
    public class FollowCamera : MonoBehaviour
    {
        //reference to the camera that the object has to follow.
        //e.g. Lifebar on the prisoner that follows the camera
        private Camera cam;

        // Start is called before the first frame update
        void Awake()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)));
        }
    }
}
