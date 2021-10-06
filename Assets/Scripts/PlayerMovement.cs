using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 mouseSpeed;
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
        vertMouse += Input.GetAxis("Mouse X") * mouseSpeed.x;
        horzMouse += Input.GetAxis("Mouse Y") * mouseSpeed.y;
        Debug.Log("vert:" + vertMouse);
        cam.localEulerAngles = new Vector3(-horzMouse, vertMouse, 0);
    }
}
