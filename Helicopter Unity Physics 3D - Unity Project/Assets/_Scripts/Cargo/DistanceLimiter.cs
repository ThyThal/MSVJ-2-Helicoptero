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
    [SerializeField] private float startPushMultiplier;
    

    //[SerializeField] private float maxDistance;

    private Rigidbody body;
    private float distance;
    private float desiredDistance;
    private float startPushDistance;
    private float totalDistanceDif;

    //Relative body velocity calculation
    //private 

    //Forces
    private Vector3 appliedSpeedForce;
    private Vector3 appliedDirectionForce;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
        desiredDistance = Vector3.Distance(transform.position, connectedBody.transform.position);
        startPushDistance = desiredDistance * startPushMultiplier;
        totalDistanceDif = desiredDistance - startPushDistance;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate relative velocity of the cargo
        //TO DO
        //connectedBody.velocity - body.velocity

        distance = Vector3.Distance(transform.position, connectedBody.transform.position);
        if (distance > startPushDistance)
        {
            Vector3 direction = (connectedBody.transform.position - transform.position).normalized;
            float distanceDifference = distance - desiredDistance;
            float dot = Vector3.Dot(body.velocity, direction);
            float distanceMult = GetForceMultiplier(distance);
            if (Limiter == LimiterMode.AtoB)
            {
                appliedSpeedForce = direction * distanceDifference * directionForceMultiplier;
                body.AddForce(appliedSpeedForce * distanceMult, ForceMode.Impulse);
                appliedDirectionForce = -direction * dot * speedForceMultiplier;
                body.AddForce(appliedDirectionForce * distanceMult, ForceMode.Impulse);
            }
            else
            {
                appliedSpeedForce = -direction * distanceDifference * directionForceMultiplier;
                connectedBody.AddForce(appliedSpeedForce * distanceMult, ForceMode.Impulse);
                appliedDirectionForce = -direction * dot * speedForceMultiplier;
                connectedBody.AddForce(appliedDirectionForce * distanceMult, ForceMode.Impulse);
            }
        }
        else
        {
            appliedSpeedForce = Vector3.zero;
            appliedDirectionForce = Vector3.zero;
        }
    }

    private float GetForceMultiplier(float currentDist)
    {
        return (currentDist - startPushDistance) / (totalDistanceDif);
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
