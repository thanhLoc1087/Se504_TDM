using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript: MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint= 3.0f;
    public float currentSpeed = 0f;
    public float currentSprint = 0f;

    [Header("Player Camera")]
    public Transform playerCamera;

    [Header("Player Animator and Gravity")]
    public CharacterController cc;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Jumping & Velocity")]
    public float jumpRange = 1f;
    public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;

    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y <0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        playerMove();

        Jump();

        Sprint();
    }

    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Running", false);
            animator.SetBool("Idle", false);
            animator.SetBool("AimWalk", false);
            animator.SetBool("IdleAim", false);
            animator.SetTrigger("Jump");

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cc.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
            currentSpeed = playerSpeed;
        } 
        else
        {
            animator.SetBool("Idle", true);
            animator.SetTrigger("Jump");
            animator.SetBool("AimWalk", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            currentSpeed = 0f;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Walk", false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        } 
        else
        {
            animator.ResetTrigger("Jump");
        }
    }

    void Sprint()
    {
        if (Input.GetButton("Sprint") && 
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) &&
            onSurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Running", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("IdleAim", false);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cc.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
                currentSprint = playerSprint;
            } 
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                currentSprint = 0f;
            }
        }
    }

    // player hit damange
    // player hit die
}
