using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class MainMenuControl : MonoBehaviour
{
    public PlayableDirector StartCine;
    public PlayableDirector StartMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnYes()
    {
        StartCine.Stop();
        StartMenu.Play();
    }
}
