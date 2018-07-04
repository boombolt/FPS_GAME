using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    void Start()
    {
        controller = GetComponent<CharacterController>();
        fpsCam = GetComponentInChildren<Camera>();
        currentSpeed = speed;

        if (!InGameMenu.IsOn)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //Public Variables
    public Camera fpsCam;
    public float speed = 6.0F;
    public float runSpeed = 1.5f;
    public float jumpSpeed = 8.0F;
    public float crouch = .5f;
    public float gravity = 20.0F;
    public float minYaw = -60;
    public float maxYaw = 60;
    public float mouseSensitivity = 5;
    public int jumpCounter = 1;

    //Private Variables
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private float xRot, yRot;
    private float yInvert = -1f;
    private float currentPitch = 0;
    private int jumpCount = 1;
    private float currentSpeed;

    void FixedUpdate()
    {
        //if (InGameMenu.IsOn)
        //    return;

        MovementControl();
        ViewControl();
    }

    void Update()
    {
        Jump();
        Run();
        //Crouch();
    }

    private void MovementControl()
    {

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= currentSpeed;

            jumpCount = jumpCounter;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void ViewControl()
    {
        xRot = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yRot = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * yInvert;

        // Player and Camera Rotates Horizontal.
        transform.Rotate(Vector3.up * xRot);

        //Camera Rotates Vertical.
        currentPitch = Mathf.Clamp(currentPitch + yRot, minYaw, maxYaw);
        fpsCam.transform.localEulerAngles = Vector3.right * currentPitch;

    }

    private void Jump()
    {
        if (jumpCount > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                controller.Move(moveDirection * Time.deltaTime);
                jumpCount--;
            }
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = speed * runSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
    }

    //private void Crouch()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftControl))
    //    {
    //        fpsCam.transform.position = new Vector3(fpsCam.transform.position.x, fpsCam.transform.position.y - crouch, fpsCam.transform.position.z);
    //    }
    //    else if (Input.GetKeyUp(KeyCode.LeftControl))
    //    {
    //        fpsCam.transform.position = new Vector3(fpsCam.transform.position.x, fpsCam.transform.position.y + crouch, fpsCam.transform.position.z);
    //    }
    //}
}
