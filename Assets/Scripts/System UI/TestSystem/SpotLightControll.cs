using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightControll : MonoBehaviour
{
    public GameObject[] lights;
    AudioSource audioSource;
    public AudioClip audioClip;
    StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        stageManager.OnPlayerJoin += Light;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        stageManager.OnPlayerJoin -= Light;
    }

    void Light(GameObject player, int n)
    {
        lights[n].SetActive(true);
        audioSource.PlayOneShot(audioClip);
    }
}
