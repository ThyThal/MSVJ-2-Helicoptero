﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public float spreadRadius = 5f;
    public GameObject fire;

    private void Start()
    {
        fire = gameObject;
        StartCoroutine(nameof(CheckFlammableNeighbors));
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(220, 0, 0, 192);
    //    Gizmos.DrawWireSphere(transform.position, spreadRadius);
    //}

    private void SpreadFire()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, spreadRadius, ~0, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < colls.Length; i++)
        {
            Flammable flammable = colls[i].GetComponent<Flammable>();
            if (flammable == null || flammable.OnFire == true) continue;
            
            if (!flammable.IsWet)
                flammable.Ignite(Instantiate(fire, flammable.transform.position, Quaternion.identity, flammable.transform).GetComponent<FireScript>());
        }
    }

    private IEnumerator CheckFlammableNeighbors()
    {
        yield return new WaitForSeconds(3);
        SpreadFire();
    }
}