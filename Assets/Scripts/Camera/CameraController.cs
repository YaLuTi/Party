using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static float ShakeDecay = 7.5f; 
    static CinemachineVirtualCamera virtualCamera;
    static CinemachineBasicMultiChannelPerlin perlin;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        // virtualCamera.Follow = GameObject.FindGameObjectWithTag("CineGroup").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(perlin.m_AmplitudeGain > 0)
        {
            perlin.m_AmplitudeGain -= Time.deltaTime * ShakeDecay;
            perlin.m_AmplitudeGain = Mathf.Max(perlin.m_AmplitudeGain, 0);
        }
    }

    public static void CameraShake(float power)
    {
        if (perlin.m_AmplitudeGain > 10) power /= 5;
        perlin.m_AmplitudeGain += power;
    }
}
