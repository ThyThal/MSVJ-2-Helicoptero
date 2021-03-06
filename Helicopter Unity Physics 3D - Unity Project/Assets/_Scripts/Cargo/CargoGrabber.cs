using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoGrabber : MonoBehaviour
{
    [SerializeField] private float grabRadius;
    [SerializeField] private LayerMask grabMask;
    [SerializeField] private KeyCode grabKey;
    [SerializeField] private Rigidbody parentRb;
    [SerializeField] private Transform parent;
    //[SerializeField] private GameObject cargo;
    [SerializeField] private FixedJoint joint;
    //[SerializeField] private HingeJoint hingeChopper;
    private bool hasCargo;
    private Collider[] colliders;
    private Transform targetGrab;
    private Rigidbody targetBody;
    private bool hasTank;
    private bool hasBox;

    private CargoForceBalancer cargoDistanceScript;

    public bool HasTank => hasTank;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(parentRb.centerOfMass.ToString());
        parent.localPosition = new Vector3(parentRb.centerOfMass.x,parentRb.centerOfMass.y - 0.5f,parentRb.centerOfMass.z);
        hasCargo = false;
        hasTank = false;
        hasBox = false;
        Debug.Log(parent.localPosition.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CoM X: " + parentRb.centerOfMass.x.ToString());
        //Debug.Log("CoM Z: " + parentRb.centerOfMass.z.ToString());

        if (Input.GetKeyDown(grabKey))
        {
            CheckForCargo();
        }

        if (hasCargo)
        {
            targetGrab.position = parent.transform.position;
            //targetGrab.position = new Vector3(parentRb.centerOfMass.x,parentRb.centerOfMass.y - 0.33f,parentRb.centerOfMass.z);
            targetBody.velocity = parentRb.velocity;
    //    }
    //}

    //void LateUpdate()
    //{     
    //    if (hasCargo)
    //    {
            parentRb.AddForceAtPosition(-cargoDistanceScript.GetAverageSpeedForce(), parent.transform.position, ForceMode.Impulse);
            parentRb.AddForceAtPosition(cargoDistanceScript.GetAverageDirectionForce() * 0.5f, parent.transform.position, ForceMode.Impulse);
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
            //targetGrab.position = transform.position;
            joint.connectedAnchor = parentRb.centerOfMass - transform.up * 0.5f;
            joint.connectedBody = targetBody;
            cargoDistanceScript = targetGrab.gameObject.GetComponent<CargoForceBalancer>();

            //cargo.transform.SetParent(parent);
            hasCargo = true;
            //hingeChopper.connectedBody = targetGrab;

            if (targetGrab.gameObject.tag == "Extinguisher")
            {
                hasTank = true;
            }
        }
        else
        {
            Debug.Log("Found nothing");
        }
    }

    private void ReleaseCargo()
    {
        hasCargo = false;
        hasBox = false;
        hasTank = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}
