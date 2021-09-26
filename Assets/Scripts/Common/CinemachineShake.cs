using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float initialShakeTimer, shakeTimer;
    private float initialIntensity;
    private bool isSmooth;
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        isSmooth = false;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                setCinemachineAmplitude(0f, this.isSmooth);
            }
        }
    }

    public void triggerShakeCamera(float intensity, float duration, bool isSmooth = false)
    {
        shakeTimer = duration;
        initialShakeTimer = duration;
        initialIntensity = intensity;
        this.isSmooth = isSmooth;
        setCinemachineAmplitude(intensity, false);
    }

    private void setCinemachineAmplitude(float intensity, bool isSmooth)
    {
        CinemachineBasicMultiChannelPerlin cinemachinePerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachinePerlin.m_AmplitudeGain =  isSmooth
            ? Mathf.Lerp(initialIntensity, 0f, shakeTimer / initialShakeTimer)
            : intensity;
    }
}
