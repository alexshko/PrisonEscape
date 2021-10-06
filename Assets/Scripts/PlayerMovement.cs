using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 mouseSpeed;
    public float MouseAimMaxAngle = 90;
    public float MouseAimMinAngle = -90;
    //sum the change in the mouseAim:
    private float vertMouse;
    private float horzMouse;

    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        vertMouse = cam.localEulerAngles.y;
        horzMouse = cam.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        MakeMouseMove();
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
