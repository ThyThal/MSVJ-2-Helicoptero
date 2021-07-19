using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CargoController : MonoBehaviour
{
    //[SerializeField] private CargoChecker cargocheck;
    [SerializeField] private TextMeshProUGUI cargoCheck;

    private bool isDelivered;

    // Start is called before the first frame update
    void Start()
    {
        isDelivered = false;
        cargoCheck.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDelivered && cargoCheck.IsActive() == false)
        {
            cargoCheck.gameObject.SetActive(true);
        }
    }

    public void DeliverCargo()
    {
        isDelivered = true;
    }
}
