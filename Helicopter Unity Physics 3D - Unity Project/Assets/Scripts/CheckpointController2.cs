using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckpointController2 : MonoBehaviour
{
    [SerializeField] private Waypoint[] waypoints;
    [SerializeField] private TextMeshProUGUI passText;
    [SerializeField] private TextMeshProUGUI checkText;

    private int totalWaypoints;
    private int maxWaypoints;

    private bool waypointsCleared;
    public bool WaypointsCleared => waypointsCleared;

    // Start is called before the first frame update
    void Start()
    {
        passText.gameObject.SetActive(false);
        totalWaypoints = waypoints.Length;
        maxWaypoints = waypoints.Length;
        waypointsCleared = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalWaypoints <= 0)
        {
            waypointsCleared = true;
            passText.gameObject.SetActive(true);
        }
        checkText.text = "Remaining Checkpoints: " + (totalWaypoints);
    }

    public void WaypointChecked()
    {
        totalWaypoints--;
    }
}
