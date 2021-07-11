using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    [Header("Helicopter Components")]
    [SerializeField] private Helicopter_Inputs.Helicopter_Main_Input_Manager mainInput;
    [SerializeField] private Helicopter_Controllers.Helicopter_Main_Controller helicopterController;
    [SerializeField] private Transform helicopterTransform;
    [SerializeField] private Rigidbody helicopterRigidbody;
    
    [Header("Sliders")]
    [SerializeField] private Slider collSlider;
    [SerializeField] private Slider altimeterSlider;
    [SerializeField] private Slider rpmSlider;
    
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI collDebug;
    [SerializeField] private TextMeshProUGUI rpmDebug;
    [SerializeField] private TextMeshProUGUI speedText;

    private bool isConfigured = false;
    private float itc_RPM;
    private float itc_Coll;

    // Start is called before the first frame update
    void Start()
    {
       if (helicopterController != null && mainInput != null &&
           collSlider != null && rpmSlider != null && 
           collDebug != null && rpmDebug != null &&
           speedText != null &&
           helicopterTransform != null && helicopterRigidbody != null)
        {
            isConfigured = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        altimeterSlider.value = helicopterTransform.position.y;
        
        if (isConfigured)
        {
            //Input Collective: clamped -1f to 1f
            collDebug.text = (mainInput.StickyCollectiveInput * 100f).ToString();
            collSlider.value = Mathf.Clamp(mainInput.StickyCollectiveInput * 100f, 1f, 100f);
            
            //CurrentRPM: 0 to 2700
            rpmDebug.text = helicopterController.GetCurrentRPM().ToString();
            rpmSlider.value = Mathf.Clamp(helicopterController.GetCurrentRPM(), 0f, 2700f);
            
            //Current rigidbody speed
            speedText.text = "Speed \t" + (Mathf.Round((helicopterRigidbody.velocity.magnitude / 1000f) * 60 * 60)).ToString()  + " Km/h";
        }
    }
}
