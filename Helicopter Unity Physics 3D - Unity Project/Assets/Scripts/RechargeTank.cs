using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeTank : MonoBehaviour
{
    [SerializeField] private float gathererSize;
    [SerializeField] private KeyCode activatorKey;
    [SerializeField] private WaterSpray waterContainer;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private LayerMask waterMask;
    [SerializeField] private float timeToCharge1;

    private bool systemOn;
    private RaycastHit hitInfo;
    private float elapsedTime;
    private Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        systemOn = false;
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activatorKey))
        {
            systemOn = !systemOn;
            elapsedTime = timeToCharge1;
        }
        if (systemOn)
        {
            ChargeTank();
        }
    }

    private void ChargeTank()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeToCharge1)
        {
            Debug.Log("tries recharge");
            //if (Physics.Raycast(rayOrigin.position, -Vector3.up, out hitInfo, gathererSize, LayerMask.NameToLayer(waterMask), QueryTriggerInteraction.Ignore))
            //{
            //    Debug.Log("Impact, recharges");
            //    waterContainer.ChargeTank(1);
            //}
            if (Physics.CheckSphere(rayOrigin.position, gathererSize, waterMask))
            {
                //colliders = Physics.OverlapSphere(transform.position, gathererSize, LayerMask.NameToLayer(waterMask), QueryTriggerInteraction.Ignore);
                waterContainer.ChargeTank(1);
                elapsedTime = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(rayOrigin.position, gathererSize);
    }
}
