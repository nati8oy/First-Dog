using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{



    public PlayerController controller;

    //pull object items
    private GameObject heldObj;
    private float pickUpRange = 20;
    public Transform holdParent;
    private float moveForce = 500;
    private Rigidbody heldRig;
    public GameObject heldItem;

    public Vector3 Direction { get; set; }

    //used to set the layer on which objects will be interactive. Change in editor. 
    [SerializeField] LayerMask interactionMask;
    [SerializeField] float raycastDistance;


    //check if the player is within range to interact with an object
    private bool canInteract;


    //reference to the player rigid body
    private Rigidbody playerRig;
    //play ray cast to see if the object in front of hit has been hit
    private RaycastHit hitObject;
    private  Rigidbody hitObjRig;



    // Start is called before the first frame update
    void Start()
    {
        playerRig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float holdObjX = holdParent.transform.position.x;
        //float boxPosX = hitObject.transform.position.x;


        //////////
        ///RAYCAST VERSION
        /////
        
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hitObject, raycastDistance, interactionMask);


        //checks the player direction so that the raycast is always pointing inwards toward the screen
        if (controller.playerDirection == 1f)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * raycastDistance, Color.red);

        }

        else if (controller.playerDirection==-1f)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * raycastDistance, Color.red);

        }
        

        //additional raycast to show where the front point of the player is pointing
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.green);


        //checks if the raycast is hitting an object and that the player is close enough to start interacting with it.
        if (Input.GetKeyDown(KeyCode.Return) && canInteract)
        {

                print("raycast hit " + hitObject.transform.gameObject.name);

                
                hitObjRig = hitObject.transform.GetComponent<Rigidbody>();
            //hitObjRig.isKinematic = false;



            hitObject.transform.parent = transform;

            //hitObject.transform.position = new Vector3(holdObjX, gameObject.transform.position.y, gameObject.transform.position.y);

            hitObject.transform.position = new Vector3 (holdObjX, hitObject.transform.position.y, hitObject.transform.position.z) ;

            //hitObjRig.transform.position = new Vector3(holdParent.transform.position.x, hitObjRig.transform.position.y, holdParent.transform.position.z);

            //hitObject.transform.position = holdParent.transform.position;

        }

        if (Input.GetKeyUp(KeyCode.Return))
        {

            hitObject.transform.parent = null;
            hitObjRig.isKinematic = true;
            //hitObject.transform.SetParent(gameObject.transform, true);
        }

        


    }

    private void OnTriggerEnter(Collider other)
    {
        canInteract = true;

    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;

    }


}

