using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fireCount;
    [SerializeField] private TextMeshProUGUI fireCheck;
    [SerializeField] private ExtinguishableFire[] fireList;

    private int totalFires;

    // Start is called before the first frame update
    void Start()
    {
        fireCheck.gameObject.SetActive(false);
        totalFires = fireList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalFires <= 0)
        {
            fireCheck.gameObject.SetActive(true);
        }
        fireCount.text = "Fires remaining: " + totalFires; 
    }

    public void PutOutFire()
    {
        totalFires--;
    }
}
