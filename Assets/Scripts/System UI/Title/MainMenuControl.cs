using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
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
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.buttonNorth.wasPressedThisFrame && !FacilityManager.IsMenu)
        {
            Debug.Log("TEST");
            StartCine.Stop();
            StartMenu.Play();
            FacilityManager.IsMenu = true;
        }
    }

    void OnYes()
    {
    }
}
