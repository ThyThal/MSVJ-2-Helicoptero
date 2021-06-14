using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoGrabber : MonoBehaviour
{
    [SerializeField] private float grabRadius;
    [SerializeField] private LayerMask grabMask;
    [SerializeField] private KeyCode grabKey;
    [SerializeField] private Rigidbody parentRb;
    //[SerializeField] private HingeJoint hingeChopper;
    private bool hasCargo;
    private Collider[] colliders;
    private Transform targetGrab;
    private Rigidbody targetBody;

    // Start is called before the first frame update
    void Start()
    {
        hasCargo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grabKey))
        {
            CheckForCargo();
        }

        if (hasCargo)
        {
            targetGrab.position = transform.position;
            targetBody.velocity = parentRb.velocity;
        }
    }

    private void CheckForCargo()
    {
        if (hasCargo)
        {
            ReleaseCargo();
        }
        else
        {
            GrabCargo();

        }
    }

    private void GrabCargo()
    {
        if (Physics.CheckSphere(transform.position, grabRadius, grabMask))
        {
            Debug.Log("Found a grabbable");
            colliders = Physics.OverlapSphere(transform.position, grabRadius, grabMask, QueryTriggerInteraction.Ignore);
            targetGrab = colliders[0].attachedRigidbody.transform;
            targetBody = colliders[0].attachedRigidbody;
            hasCargo = true;
            //hingeChopper.connectedBody = targetGrab;
        }
        else
        {
            Debug.Log("Found nothing");
        }
    }

    private void ReleaseCargo()
    {
        hasCargo = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}
