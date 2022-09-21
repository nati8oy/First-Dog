using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private GameObject heldObj;
    private float pickUpRange = 20;
    public Transform holdParent;
    private float moveForce = 250;
    private Rigidbody heldRig;
    public float forceMagnitude;


    void MoveObject()
    {

        if (Vector3.Distance(heldObj.transform.position, holdParent.position) > 0.1f)
        {
            Vector3 moveDirection = (holdParent.position - heldObj.transform.position);
            heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;

            objRig.transform.parent = holdParent;
            heldObj = pickObj;
            print("picked up");

        }

    }

    void DropObject()
    {
        heldRig = heldObj.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;

        heldRig.transform.parent = null;
        //heldObj = null;
        print("dropped");
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J))
        {


            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    PickUpObject(hit.transform.gameObject);
                    print(hit.transform.gameObject.name.ToString());
                }

                else
                {
                    DropObject();
                }
            }

            if (heldObj != null)
            {
                MoveObject();
            }


        }

    }


}
