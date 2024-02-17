using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float xAxis;
    float yAxis;
    public float speed;
    private const float baseSpeed = 15;

    float mouseX;
    float mouseY;
    public float sensitivity;
    float mouseVerticalRotation;

    float playerHeight;
    public LayerMask groundLayer;
    bool isGrounded;
    float groundDrag;

    bool isCrawling = false;
    bool isJumping = false;
    float upForce = 0;
    float airMultiply = 0.4f;

    Camera camera;
    CapsuleCollider capsuleCollider;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        sensitivity = 150;

        camera = Camera.main;
        Cursor.visible = false;

        capsuleCollider = GetComponent<CapsuleCollider>();

        rigidbody = GetComponent<Rigidbody>();

        playerHeight = capsuleCollider.height;
        groundDrag = 10;
        upForce = 15;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        if (isGrounded)
            rigidbody.drag = groundDrag;
        else
            rigidbody.drag = 0;

        if(Input.GetKeyDown(KeyCode.LeftShift))
            speed = 25;

        if(Input.GetKeyUp(KeyCode.LeftShift))
            speed = baseSpeed;

        if(Input.GetKeyDown(KeyCode.C))
            Crawle();


        
        CameraMove();

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded)
        {
            Jump();

            Invoke(nameof(ResetJump), 0.5f);
        }

        FixSpeed();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void CameraMove()
    {
        mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        mouseVerticalRotation += -mouseY;
        mouseVerticalRotation = Mathf.Clamp(mouseVerticalRotation, -90f, 90f);
        camera.transform.localEulerAngles = new Vector3(mouseVerticalRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void PlayerMove()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xAxis + transform.forward * yAxis;
        //characterController.Move(move * speed * Time.deltaTime);
        if (isGrounded)
        {
            rigidbody.AddForce(move.normalized * speed * 10, ForceMode.Force);

        }
        else if (!isGrounded)
        {
            rigidbody.AddForce(move.normalized * baseSpeed * 10 * airMultiply, ForceMode.Force);

        }
    }

    private void Crawle()
    {
        if(!isCrawling)
        {
            isCrawling = true;

            capsuleCollider.height -= 2;
            playerHeight = capsuleCollider.height;
            speed = 3;
        }
        else
        {
            isCrawling = false;

            capsuleCollider.height += 2;
            playerHeight = capsuleCollider.height;
            speed = baseSpeed;
        }
    }

    void Jump()
    {
        isJumping = true;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        rigidbody.AddForce(transform.up * upForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        isJumping = false;
    }

    void FixSpeed()
    {
        Vector3 vel = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        if (vel.magnitude > speed)
        {
            Vector3 limitetVel = vel.normalized * speed;
            rigidbody.velocity = new Vector3(limitetVel.x, rigidbody.velocity.y, limitetVel.z);
        }
    }
}
