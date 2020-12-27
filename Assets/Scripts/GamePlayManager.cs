using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TouchField touchField;
    [SerializeField] private GvrEditorEmulator gvrEditorEmulator;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSensitivity;
    private float xRotation, yRotation;
    [SerializeField] private int inputType;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            inputType = GameManager.Instance.data.InputType;
        }

        StartCoroutine(inputType == 0 ? LoadDevice("cardboard") : LoadDevice("None"));
    }

    private void Update()
    {
        switch (inputType)
        {
            case 1:
                gvrEditorEmulator.enabled = false;
                Input.gyro.enabled = true;
                cameraTransform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y,
                    Input.gyro.rotationRateUnbiased.z);
                break;
            case 2:
                gvrEditorEmulator.enabled = false;
                xRotation += (touchField.TouchDist.y * cameraSensitivity * Time.deltaTime);
                yRotation -= (touchField.TouchDist.x * cameraSensitivity * Time.deltaTime);
                xRotation = Mathf.Clamp(xRotation, -90, 90f);
                cameraTransform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
                break;
        }
    }

    IEnumerator LoadDevice(string newDevice)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = true;
    }
}