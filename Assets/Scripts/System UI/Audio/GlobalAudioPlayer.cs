using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioPlayer : MonoBehaviour
{
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayRespawn()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
