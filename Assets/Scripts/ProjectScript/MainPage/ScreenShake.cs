using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 originalCameraPosition;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;
    private float dampingSpeed = 1.0f;

    void Awake()
    {
        EventCenter.GetInstance().AddEventListener("ScreenShake",StartShake);

    }

    void Update()
    {
        if (cameraTransform != null)
        {
            if (shakeDuration > 0)
            {
                cameraTransform.localPosition = originalCameraPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                cameraTransform.localPosition = originalCameraPosition;
            }
        }
    }

    public void StartShake()
    {
        originalCameraPosition = cameraTransform.localPosition;
        shakeDuration = 0.8f;
        shakeMagnitude = 1.5f;
    }
}
