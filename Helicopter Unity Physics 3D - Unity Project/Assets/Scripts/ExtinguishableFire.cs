using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishableFire : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire1;
    [SerializeField] private ParticleSystem fire2;
    [SerializeField] private FireController fireControl;
    [SerializeField] private string waterMask;
    [SerializeField] private int splashesToExtinguish;

    private int impacts;
    private int halfImpacts;
    private bool fireIsOn;

    // Start is called before the first frame update
    void Start()
    {
        fireIsOn = true;
        Debug.Log(LayerMask.NameToLayer(waterMask));
        if (splashesToExtinguish < 2)
        {
            splashesToExtinguish = 2;
        }
        impacts = 0;
        halfImpacts = splashesToExtinguish / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(waterMask)) 
        {
            impacts++;
            //Debug.Log("Impacts: " + impacts.ToString());
            Destroy(other.gameObject);
            if (impacts > halfImpacts && impacts < splashesToExtinguish)
            {
                fire1.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            if (impacts > splashesToExtinguish && fireIsOn)
            {
                fireIsOn = false;
                fire2.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                fireControl.PutOutFire();

            }
        }
    }
}
