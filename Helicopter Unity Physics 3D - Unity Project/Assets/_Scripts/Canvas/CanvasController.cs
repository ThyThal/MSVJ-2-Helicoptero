using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    //[SerializeField] private Helicopter_Inputs.Helicopter_Keyboard_InputManager inputManager;
    [SerializeField] private Helicopter_Inputs.Helicopter_Main_Input_Manager mainInput;
    [SerializeField] private Helicopter_Controllers.Helicopter_Main_Controller heliController;
    [SerializeField] private Transform heliTransform;
    [SerializeField] private Rigidbody heliRB;
    [SerializeField] private Slider collSlider;
    [SerializeField] private Slider rpmSlider;
    [SerializeField] private TextMeshProUGUI collDebug;
    [SerializeField] private TextMeshProUGUI rpmDebug;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI altitudeText;

    private bool isConfigured = false;
    private float itc_RPM;
    private float itc_Coll;

    // Start is called before the first frame update
    void Start()
    {
       if (heliController != null && mainInput != null &&
           collSlider != null && rpmSlider != null && 
           collDebug != null && rpmDebug != null &&
           speedText != null && altitudeText != null &&
           heliTransform != null && heliRB != null)
        {
            isConfigured = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (isConfigured)
        {
            //Input Collective: clamped -1f to 1f
            collDebug.text = (mainInput.StickyCollectiveInput * 100f).ToString();
            collSlider.value = Mathf.Clamp(mainInput.StickyCollectiveInput * 100f, 1f, 100f);
            //CurrentRPM: 0 to 2700
            rpmDebug.text = heliController.GetCurrentRPM().ToString();
            rpmSlider.value = Mathf.Clamp(heliController.GetCurrentRPM(), 0f, 2700f);

            //Current rigidbody speed
            speedText.text = "Speed \t" + (Mathf.Round((heliRB.velocity.magnitude / 1000f) * 60 * 60)).ToString()  + " Km/h";
            //Current transform position Y for Altitude
            altitudeText.text = "Altitude \t" + Mathf.Round(heliTransform.position.y).ToString() + " m";
        }
    }
}
