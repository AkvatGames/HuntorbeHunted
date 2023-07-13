using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maximumSlowdown;
    private float currentSlowdown;
    public float speed;
    public float walkSpeed = 10;
    public float runSpeed = 20;
    public float strafeSlowdown = 0.5f;
    public float jumpSpeed = 25;
    public float gravity = 25;
    public float stamina = 100;
    public Camera playerCamera;
    public float lookSettings { get; set; }
    public float lookSpeed = 3;
    public float lookXLimit = 45;
    public bool isJumping;
    public bool invertLook;
    //public InputAction playerInput;
    public bool canRun, canJump;
    public float airSlowDown;

    //public bool canHeal;
    //public bool canFire { get; set; }
    //public GameObject JumpVolume;

    bool isRunning = false;

    //Vector3 targetRotation;

    //SC_DamageReceiver player;
    //JW_PauseMenu menus;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    public Vector2 rotation = Vector2.zero;

    [HideInInspector]
    public bool canMove = true;
    // Setup player hiding via ducking or not running/shooting
    public bool isVisible;
    public bool isAudible;
    public bool isDetected;

    //public GroundCheck groundChecker;

    private Vector3 movement;

    public void Initialize()
    {
        lookSpeed = lookSettings;
    }

    void Start()
    {
        lookSettings = 5.0f;
        gameObject.tag = "Player";
        isVisible = true;
        isAudible = !isAudible;

        runSpeed = Mathf.Clamp(runSpeed, 0, runSpeed);
        walkSpeed = Mathf.Clamp(walkSpeed, 0, walkSpeed);




        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;

        //Set visible states
        if (isVisible | isAudible == true)
        {
            isDetected = true;
        }
    }

    void Update()
    {
        if (stamina <= 0)
        {
            canRun = false;
            canJump = false;
        }
        else if (stamina > 0)
        {
            canRun = true;
            canJump = true;
        }

        speed = Mathf.Clamp(speed, 0, runSpeed);

        if (characterController.isGrounded)
        //if (groundChecker.isGrounded)
        {
            isJumping = false;
            // We are grounded, so recalculate move direction based on axes
            //Vector3 forward = transform.TransformDirection(Vector3.forward);
            //Vector3 right = transform.TransformDirection(Vector3.right);
            DirectionSlowdown();
            //float curSpeedX = canMove ? (speed * DirectionSlowdown()) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (speed * strafeSlowdown) * Input.GetAxis("Horizontal") : 0;
            //moveDirection = (forward.normalized * curSpeedX) + (right.normalized * curSpeedY);
            //moveDirection = (curSpeedX * forward) + (right * curSpeedY);


            if (speed < (runSpeed - walkSpeed))
            {
                speed = 0;
            }

            if (Input.GetButton("Run") && canMove && canRun && stamina > 0)
            {
                //speed = runSpeed;//Mathf.Lerp(speed, runSpeed, Time.deltaTime * 20);
                speed = Mathf.Clamp(speed, runSpeed, runSpeed);
                //stamina -= stamina * Time.deltaTime;
                isAudible = true;
                isRunning = true;
                //canFire = false;

            }
            else if (!Input.GetButton("Run") && canMove)
            {
                speed = walkSpeed;//Mathf.Lerp(speed, walkSpeed, Time.deltaTime * 10);
                speed = Mathf.Clamp(speed, walkSpeed, walkSpeed);
            }

            else if (Input.GetButtonDown("Jump") && canMove && canJump && stamina > 0)
            {
                while (isJumping == true)
                {
                    AirSlowdown();
                    moveDirection.y -= gravity * Time.deltaTime;
                }
                //isJumping = true;
                //Time.timeScale = 0f;
                //stamina -= stamina * Time.deltaTime;
                moveDirection.y = jumpSpeed;
                isAudible = true;
                //StartCoroutine(SlowJump());
            }

            else if(Input.GetButtonDown("Jump") && isRunning == true)
            {
                StartCoroutine(SlowJump());
            }


        }

        //if (!isJumping && !groundChecker.isGrounded)
        if (!isJumping && !characterController.isGrounded)
        {
            moveDirection.y -= (gravity) * Time.deltaTime;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= (gravity) * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);


        // Player and Camera rotation
        if (canMove)
        {

            rotation.y += Input.GetAxis("Mouse X") * lookSettings * 50 * Time.unscaledDeltaTime;

            if (invertLook == false)
            {
                rotation.x += -Input.GetAxis("Mouse Y") * lookSettings * 50 * Time.unscaledDeltaTime;
            }
            if (invertLook == true)
            {
                rotation.x += Input.GetAxis("Mouse Y") * lookSettings * 50 * Time.unscaledDeltaTime;
            }
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector3(0, rotation.y, 0);
        }
    }

    public void InvertLook()
    {
        if (invertLook == false)
        {
            invertLook = true;
        }
        else
        {
            invertLook = false;
        }

    }

    public void DirectionSlowdown()
    {
        //characterController.isGrounded = true;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedY = canMove ? (speed * strafeSlowdown) * Input.GetAxis("Horizontal") : 0;
        if (Input.GetAxis("Vertical") >= 0)
        {
            float curSpeedX = canMove ? (speed) * Input.GetAxis("Vertical") : 0;
            moveDirection = (curSpeedX * forward) + (right * curSpeedY);

        }
        else
        {
            float curSpeedX = canMove ? (speed * 0.5f) * Input.GetAxis("Vertical") : 0;
            moveDirection = (curSpeedX * forward) + (right * curSpeedY);
        }

    }

    public void AirSlowdown()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedY = canMove ? (speed * strafeSlowdown) * Input.GetAxis("Horizontal") : 0;
        if (Input.GetAxis("Vertical") >= 0)
        {
            float curSpeedX = canMove ? (speed) * Input.GetAxis("Vertical") : 0;
            moveDirection = (curSpeedX * forward) + (right * curSpeedY);

        }
        else
        {
            float curSpeedX = canMove ? (speed * 0.5f) * Input.GetAxis("Vertical") : 0;
            moveDirection = (curSpeedX * forward) + (right * curSpeedY);
        }

    }

    public IEnumerator SlowJump()
    {
        Time.timeScale = 0.65f;
        moveDirection.y = jumpSpeed;
        isJumping = true;
        yield return new WaitForSeconds(currentSlowdown);
        Time.timeScale = 1f;
    }
}
