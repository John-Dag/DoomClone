using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float jumpSpeed;
    public float gravity;
    public float mouseSensitivity = 5.0f;
    public float mouseSmoothing = 2.0f;
    public bool isFiring;
    public float health;
    private Vector3 moveDirection;
    private Vector2 mouseLook;
    private Vector2 inputMouseAxis;
    private Vector2 smoothV;
    private CharacterController controller;
    private float timer;

    void Awake()
    {
        speed = 6.0f;
        jumpSpeed = 8.0f;
        gravity = 20.0f;
        moveDirection = Vector3.zero;
        inputMouseAxis = new Vector3(0.0f, 0.0f);
        smoothV = new Vector3(0.0f, 0.0f, 0.0f);
        controller = GetComponent<CharacterController>();
        isFiring = false;
        timer = 0.0f;
        health = 100f;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        inputMouseAxis.Set(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        inputMouseAxis = Vector2.Scale(inputMouseAxis, new Vector2(mouseSensitivity * mouseSmoothing,
                                       mouseSensitivity * mouseSmoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, inputMouseAxis.x, 1f / mouseSmoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, inputMouseAxis.y, 1f / mouseSmoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90.0f, 90.0f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.up);
        controller.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, controller.transform.up);

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
