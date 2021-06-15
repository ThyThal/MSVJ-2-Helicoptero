using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLimiter : MonoBehaviour
{
    private enum LimiterMode { AtoB, BtoA };

    [SerializeField] private Rigidbody connectedBody;
    [SerializeField] private float directionForceMultiplier;
    [SerializeField] private float speedForceMultiplier;
    [SerializeField] private LimiterMode Limiter;
    

    //[SerializeField] private float maxDistance;

    private Rigidbody body;
    private float distance;
    private float desiredDistance;

    //Forces
    private Vector3 appliedSpeedForce;
    private Vector3 appliedDirectionForce;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        desiredDistance = Vector3.Distance(transform.position, connectedBody.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, connectedBody.transform.position);
        if (distance > desiredDistance)
        {
            Vector3 direction = (connectedBody.transform.position - transform.position).normalized;
            float distanceDifference = distance - desiredDistance;
            float dot = Vector3.Dot(body.velocity, direction);
            if (Limiter == LimiterMode.AtoB)
            {
                appliedSpeedForce = direction * distanceDifference * directionForceMultiplier;
                body.AddForce(appliedSpeedForce, ForceMode.Impulse);
                appliedDirectionForce = -direction * dot * speedForceMultiplier;
                body.AddForce(appliedDirectionForce, ForceMode.Impulse);
            }
            else
            {
               

                appliedSpeedForce = -direction * distanceDifference * directionForceMultiplier;
                connectedBody.AddForce(appliedSpeedForce, ForceMode.Impulse);
                appliedDirectionForce = -direction * dot * speedForceMultiplier;
                connectedBody.AddForce(appliedDirectionForce, ForceMode.Impulse);
            }
        }
    }

    public Vector3 GetAppliedSpeedForce()
    {
        return appliedSpeedForce;
    }

    public Vector3 GetAppliedDirectionForce()
    {
        return appliedDirectionForce;
    }
}
