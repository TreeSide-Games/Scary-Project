using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xAxis;
    float yAxis;
    public float speed;
    private const float baseSpeed = 10;

    float mouseX;
    float mouseY;
    public float sensitivity;
    float mouseVerticalRotation;

    bool isCrawling = false;
    float upMotion = 0;

    Camera camera;
    CharacterController characterController;
    CapsuleCollider capsuleCollider;
    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        sensitivity = 150;
        camera = Camera.main;

        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 18;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = baseSpeed;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Crawle();
        }



        
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

        Vector3 move = transform.right * xAxis + transform.forward * yAxis + transform.up * upMotion;

        characterController.Move(move * speed * Time.deltaTime);

        if (transform.position.y < 4.24f)
        {
            upMotion = 0;
            transform.position = new Vector3(transform.position.x, 4.24f, transform.position.z);
        }else if(transform.position.y > 5.24f)
        {
            upMotion = 0;
            transform.position = new Vector3(transform.position.x, 5.24f, transform.position.z);
        }
    }

    private void Crawle()
    {
        if(!isCrawling)
        {
            isCrawling = true;

            characterController.height -= 2;
            capsuleCollider.height -= 2;
            upMotion = -2f;

            speed = 3;
        }
        else
        {
            isCrawling = false;

            characterController.height += 2;
            capsuleCollider.height += 2;
            upMotion = 2f;

            speed = baseSpeed;
        }
    }
}
