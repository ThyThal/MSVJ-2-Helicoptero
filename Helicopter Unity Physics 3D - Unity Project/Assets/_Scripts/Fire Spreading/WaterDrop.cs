using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public LayerMask ignoreLayer;
    private bool flagForDeletion = false;
    private Rigidbody _RB;
    
    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        _RB.velocity = Vector3.down;
        //Physics.IgnoreLayerCollision(4, 4, true);
    }

    //private void Update()
    //{
    //    if (_RB.velocity.magnitude < 1f || transform.position.y < -25)
    //        Destroy(gameObject);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Flammable flammable = collision.gameObject.GetComponent<Flammable>();

        if (flammable != null)
            flammable.Extinguish();

        if (collision.gameObject.layer != 4) Destroy(gameObject, 1);
    }

    private void Initialize(Vector3 pos)
    {
        transform.position = pos;
        _RB.velocity = Vector3.down;
    }
}
