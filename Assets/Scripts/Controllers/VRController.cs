using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRController : UIController
{
    // Start is called before the first frame update
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.data.InputType == 0 && Input.GetMouseButtonDown(0))
            {
                ToggleVR();
            }
        }
    }

    private void ToggleVR()
    {
        StartCoroutine(XRSettings.loadedDeviceName == "cardboard" ? LoadDevice("None") : LoadDevice("cardboard"));
    }

    private IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }
}