using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SettingValue : MonoBehaviour
{
    public static float Light;
    public static float masterVolume = 1;

    public VolumeProfile processProfile;
    // ColorAdjustments colorAdjustments;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // processProfile.TryGet(out colorAdjustments);
    }

    public void ChangeVolume(float f)
    {
        masterVolume = f;
        AudioListener.volume = masterVolume;
    }

    public void ChangeLight(float f)
    {
        // colorAdjustments.postExposure.value = f;
    }
}
