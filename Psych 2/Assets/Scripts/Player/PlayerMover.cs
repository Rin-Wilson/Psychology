using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    //Private refs
    private CharacterController controller;
    private InputManager inputs;

    [Header("Movement")]
    public float walkSpeed = 2.0f;
    public float sprintSpeed = 6.0f;
    public float acceleration = 0.5f;

    private float speed;
    private Vector3 movement;
    [Header("Jump And Gravity")]
    public float jumpHeight = 5.0f;
    public float jumpTimeout = 0.5f;
    public float gravity;
    public float terminalVelocity;

    public float fallTimeout = 0.15f;
    private float fallTimeoutDelta;
    private float jumpTimeoutDelta;
    private float verticalVelocity;
    [Space(10)]
    public bool grounded;
    public float groundedDistance = 0.02f;

    [Header("Animation")]
    public Animator animator;
    public float motionSpeed = 1.0f;



    [Header("Camera")]
    public Cinemachine.CinemachineInputProvider camInputs;
    public GameObject lookCamera;
    public float lookSpeed;
    [Space(10)]
    public float rotationSmoothTime = 0.12f;
    private float rotationVelocity;

    private InputActionReference cameraInputAction;
    private float targetRotation;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputs = GetComponent<InputManager>();
        cameraInputAction = camInputs.XYAxis;
    }

    private void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        Move();
        Animate();
        CameraInputs();
    }

    private void GroundedCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, groundedDistance);
    }

    private void Animate()
    {
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Jump", inputs.jump);
        animator.SetFloat("Speed", speed);
        animator.SetFloat("MotionSpeed", motionSpeed);
    }

    private void Move()
    {
        float targetSpeed = inputs.sprint ? sprintSpeed : walkSpeed;

        float currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (inputs.move == Vector2.zero)
        {
            targetSpeed = 0;
        }

        if (currentSpeed < targetSpeed - 0.01 || currentSpeed > targetSpeed + 0.01)
        {
            speed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration);
        } 
        else
        {
            speed = targetSpeed;
        }

        //normalised direction of the inputs
        Vector3 inputDirection = new Vector3(inputs.move.x, 0.0f, inputs.move.y).normalized;

        //if there is input
        if (inputs.move != Vector2.zero)
        {
            //sets target rotation to direction in euler angles
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + lookCamera.transform.eulerAngles.y;
            
            //
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void JumpAndGravity()
    {
        if (grounded)
        {
            fallTimeoutDelta = fallTimeout;
            animator.SetBool("FreeFall", false);
            
            if (inputs.jump && jumpTimeoutDelta <= 0.0f)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (jumpTimeoutDelta >= 0.0f)
            {
                jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            if(fallTimeoutDelta >= 0)
            {
                fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("FreeFall", true);
            }
            jumpTimeoutDelta = jumpTimeout;
            inputs.jump = false;
        }

        if(verticalVelocity < terminalVelocity)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }
    
    private void CameraInputs()
    {
        camInputs.XYAxis = inputs.allowLook ? cameraInputAction : null;
    }
    
}
