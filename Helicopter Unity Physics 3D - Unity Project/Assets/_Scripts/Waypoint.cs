using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint nextWaypoint;
    [SerializeField] private Waypoint previousWaypoint;
    [SerializeField] private GameObject indicationArrows;
    [SerializeField] private bool reachedWaypoint;
    [SerializeField] private bool firstWaypoint;

    [SerializeField] public MeshRenderer meshRenderer;
    [SerializeField] private Material material;

    [ColorUsage(true, true)]
    [SerializeField] private Color enabledColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color disabledColor;

    private void Start()
    {
        Material newMaterial = new Material(material);
        newMaterial.SetFloat("_Enabled", 0);
        meshRenderer.material = newMaterial;

        if (firstWaypoint == true)
        {
            newMaterial.SetFloat("_Enabled", 1);
        }

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
        if (other.gameObject.CompareTag("Player") && reachedWaypoint == false)
        {
            if ((firstWaypoint == true && nextWaypoint != null))
            {
                reachedWaypoint = true;
                meshRenderer.material.SetFloat("_Enabled", 0);
                nextWaypoint.meshRenderer.material.SetFloat("_Enabled", 1);
                this.gameObject.SetActive(false);
            }

            else
            {
                if (previousWaypoint.reachedWaypoint == true)
                {
                    reachedWaypoint = true;
                    meshRenderer.material.SetFloat("_Enabled", 0);


                    if (nextWaypoint != null)
                    {
                        nextWaypoint.meshRenderer.material.SetFloat("_Enabled", 1);
                        this.gameObject.SetActive(false);
                    }

                    if (nextWaypoint == null)
                    {
                        Debug.Log("WIN");
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
