using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer BGMaudioMixer;
    public AudioMixer SFXaudioMixer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBGM(float f)
    {
        BGMaudioMixer.SetFloat("Volume", f);
    }
    public void SetSFX(float f)
    {
        SFXaudioMixer.SetFloat("Volume", f);
    }
}
