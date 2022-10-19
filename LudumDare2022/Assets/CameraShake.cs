using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin perlin;
    float shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        shakeTimer = Mathf.MoveTowards(shakeTimer, 0f, Time.deltaTime);
        if (shakeTimer <= 0)
        {
            perlin.m_AmplitudeGain = 0f;
        }
    }

    public void ShakeCamera(float intensity, float dur)
    {
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = dur;
    }
}
