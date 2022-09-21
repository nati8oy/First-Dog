using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 1.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    //state changes
    private string playerState;
    private bool running = false;
    private bool sneaking = false;
    public float sneakSpeed = 2;
    public float runSpeed = 2;

    public float playerDirection;

    public float forceToApply;

    private bool canDragObject = false;
    public GameObject currentDraggableObject;
    public GameObject player;
    private Rigidbody playerRigidBody;



    public float forceMagnitude;
    //how long the player can run for
    public float endurance = 25;



    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        //   playerRigidBody = currentDraggableObject.GetComponent<Rigidbody>();

    }



    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //for 2D movement use this
        Vector2 move2D = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        controller.Move(move2D * Time.deltaTime * playerSpeed);

        if (move2D != Vector2.zero)
        {
            gameObject.transform.forward = move2D;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //stateCheck(true);
            playerState = "running";
            playerSpeed += runSpeed;


        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //stateCheck(false);
            playerState = "default";
            playerSpeed -= runSpeed;

        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            // stateCheck(true);
            playerState = "sneaking";
            playerSpeed -= sneakSpeed;


        }
        else if (Input.GetKeyUp(KeyCode.RightShift))
        {// stateCheck(false);
            playerState = "default";
            playerSpeed += sneakSpeed;

        }

        //used for the raycast to make sure it is always pointing inwards
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerDirection = -1f;

        }

        if (Input.GetKeyDown(KeyCode.D)){
            playerDirection = 1f;

        }

    }

}
