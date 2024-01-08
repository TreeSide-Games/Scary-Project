using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xAxis;
    float yAxis;
    public float speed;

    float mouseX;
    float mouseY;
    public float sensitivity;
    float mouseVerticalRotation;

    Camera camera;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        speed = 15;
        sensitivity = 150;
        camera = Camera.main;

        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        
        //transform.position += new Vector3(xAxis, 0, 0) * speed * Time.deltaTime;
        
        //transform.position += new Vector3(0, 0, yAxis) * speed * Time.deltaTime;
        
        //camera.transform.LookAt(Input.mousePosition,Vector3.up);

        //transform.up = camera.transform.forward;

        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        mouseVerticalRotation += -mouseY;
        mouseVerticalRotation = Mathf.Clamp(mouseVerticalRotation, -90f, 90f);
        camera.transform.localEulerAngles = new Vector3(mouseVerticalRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);

        Vector3 move = transform.right * xAxis + transform.forward * yAxis;

        characterController.Move(move * speed * Time.deltaTime);
    }
}
