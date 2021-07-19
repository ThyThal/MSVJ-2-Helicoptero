using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoChecker : MonoBehaviour
{
    [SerializeField] private CargoController cargoCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cargo")
        {
            cargoCheck.DeliverCargo();
        }
    }
}
