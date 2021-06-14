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
                body.AddForce(direction * distanceDifference * directionForceMultiplier, ForceMode.Impulse);
                body.AddForce(-direction * dot * speedForceMultiplier, ForceMode.Impulse);
            }
            else
            {
                connectedBody.AddForce(-direction * distanceDifference * directionForceMultiplier, ForceMode.Impulse);
                connectedBody.AddForce(-direction * dot * speedForceMultiplier, ForceMode.Impulse);
            }
        }
    }
}
