﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerSlider : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevel(float sliderValue)
    {
        if (sliderValue > 0)
        {
            audioMixer.SetFloat(name, Mathf.Log10(sliderValue) * 30);
        }
        else
        {
            audioMixer.SetFloat(name, -80);
        }
    }
}
