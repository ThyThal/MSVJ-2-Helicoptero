using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint nextWaypoint;
    [SerializeField] private GameObject indicationArrows;

    private void Start()
    {
        if (nextWaypoint != null)
        {
            GetNextWaypointDirection();
        }
    }

    [ContextMenu("Get Next Direction")]
    public void GetNextWaypointDirection()
    {
        var direction = (this.transform.position - nextWaypoint.transform.position).normalized;
        var rotation = Quaternion.LookRotation(-direction);
        indicationArrows.transform.rotation = rotation;
    }
}
