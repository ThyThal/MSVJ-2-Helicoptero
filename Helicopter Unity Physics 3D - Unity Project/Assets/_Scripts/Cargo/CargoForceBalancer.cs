using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoForceBalancer : MonoBehaviour
{
    [SerializeField] private DistanceLimiter[] limiters;

    private Vector3 averageSpeedForce;
    private Vector3 averageDirectionForce;
    private Vector3 addedSpeedForce;
    private Vector3 addedDirectionForce;
    private bool hasLimiters = false;

    // Start is called before the first frame update
    void Start()
    {
        if (limiters.Length > 0)
        {
            hasLimiters = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLimiters)
        {
            addedDirectionForce = Vector3.zero;
            addedSpeedForce = Vector3.zero;
            for (int i = 0; i < limiters.Length; i++)
            {
                addedSpeedForce += limiters[i].GetAppliedSpeedForce();
                addedDirectionForce += limiters[i].GetAppliedDirectionForce();
            }
            averageDirectionForce = addedDirectionForce / (float)limiters.Length;
            averageSpeedForce = addedSpeedForce / (float)limiters.Length;
        }
    }

    public Vector3 GetAverageSpeedForce()
    {
        return averageSpeedForce;
    }

    public Vector3 GetAverageDirectionForce()
    {
        return averageDirectionForce;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawRay(transform.position, averageSpeedForce.normalized);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, averageDirectionForce.normalized);
    }
}
