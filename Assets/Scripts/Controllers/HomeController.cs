using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : UIController
{
    [SerializeField] private Text txtMagnetometer, txtAccelerometer, txtGyroscope;
    [SerializeField] private Button btnVR, btnNonVR;
    private bool magnetometer, accelerometer, gyroscope;

    private void Start()
    {
        StartCoroutine(IsMagnetometerAvailable());
        accelerometer = SystemInfo.supportsAccelerometer;
        gyroscope = SystemInfo.supportsGyroscope;
        
        txtMagnetometer.text = "Magnetometer: " + magnetometer;
        txtAccelerometer.text = "Accelerometer: " + accelerometer;
        txtGyroscope.text = "Gyroscope: " + gyroscope;

        btnVR.interactable = btnNonVR.interactable = accelerometer && gyroscope;
    }

    public void ChooseInputType(int inputType)
    {
        SceneLoader("VRScene");
        GameManager.Instance.data.InputType = inputType;
    }

    private IEnumerator IsMagnetometerAvailable()
    {
        Input.compass.enabled = true;
        for (int i = 0; i < 10; i++)
        {
            if (Input.compass.trueHeading != 0)
            {
                magnetometer = true;
            }

            yield return new WaitForSeconds(.05f);
        }
    }
}