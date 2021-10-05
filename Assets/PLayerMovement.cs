using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMovement : MonoBehaviour
{
    private float vertMouse;
    private float horzMouse;
    private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        vertMouse = Input.GetAxis("Mouse X");
        horzMouse = Input.GetAxis("Mouse Y");
        Debug.Log("vert:" + vertMouse);
        vertMouse += cam.localEulerAngles.y;
        horzMouse -= cam.localEulerAngles.x;
        cam.localEulerAngles = new Vector3(-horzMouse, vertMouse, 0);
    }
}
