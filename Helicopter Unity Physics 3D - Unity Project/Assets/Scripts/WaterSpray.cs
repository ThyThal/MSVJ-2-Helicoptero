using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour
{
    [SerializeField] private Transform waterSpawnPoint;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private float timeToTick;
    [SerializeField] private int charges;
    [SerializeField] private int maxCharges;
    [SerializeField] private ParticleSystem waterDrops;
    [SerializeField] private ParticleSystem waterSpray;
    [SerializeField] private ParticleSystem waterStream;
    [SerializeField] private KeyCode activatorKey;

    public int Charges => charges;

    public int MaxCharges => maxCharges;

    public bool IsGrabbed => IsGrabbed;

    private float timeElapsed;
    private bool systemOn;
    private bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0f;
        systemOn = false;
        isGrabbed = false;
        waterDrops.Stop();
        waterSpray.Stop();
        waterStream.Stop(false, ParticleSystemStopBehavior.StopEmitting);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activatorKey))
        {
            if (charges > 0)
            {
                systemOn = !systemOn;
                CheckSystem();
            }
        }
        if (systemOn)
        {
            DropWater();
        }
    }

    private void DropWater()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeToTick)
        {
            timeElapsed = 0f;
            charges--;
            Instantiate(waterPrefab, waterSpawnPoint.position, Quaternion.identity);
            if (charges == 0)
            {
                systemOn = !systemOn;
                CheckSystem();
            }
        }
    }

    public void ChargeTank(int amount)
    {
        charges += amount;
        if (charges > maxCharges)
        {
            charges = maxCharges;
        }
    }

    private void CheckSystem()
    {
        if (systemOn)
        {
            //If the system is started, sets the time to the target time in order to start right away spawning water.
            timeElapsed = timeToTick;
            waterDrops.Play();
            waterSpray.Play();
            waterStream.Play();
        }
        else
        {
            waterDrops.Stop();
            waterSpray.Stop();
            waterStream.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
