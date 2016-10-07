using UnityEngine;
using System.Collections;

public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -.8f;

    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        float vertSpeed = 0;
        float jumpSpeed = 100f;
        if (charController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed += gravity;
            }
        }
        else
        {
            vertSpeed += gravity;
        }
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);

    }
}
