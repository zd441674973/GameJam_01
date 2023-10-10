using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 originalCameraPosition;
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1.0f;

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
        Debug.Log("shake");

        originalCameraPosition = cameraTransform.localPosition;
        shakeDuration = 8f;
        shakeMagnitude = 15f;
    }

    public void DestroySelf()
    {
        EventCenter.GetInstance().RemoveEventListener("ScreenShake", StartShake);

        Destroy(this);
    }
}
