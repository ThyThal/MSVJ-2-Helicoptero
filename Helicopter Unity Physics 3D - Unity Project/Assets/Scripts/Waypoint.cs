using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint nextWaypoint;
    [SerializeField] private GameObject indicationArrows;
    [SerializeField] private CheckpointController2 checkController;

    private bool checkpointDone;

    public bool CheckpointDone => checkpointDone;

    private void Start()
    {
        checkpointDone = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            checkpointDone = true;
            checkController.WaypointChecked();
            this.gameObject.SetActive(false);
        }
    }
}
