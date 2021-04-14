using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;
using UnityEngine.InputSystem.Controls;

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
        bool gamepadPressed = false;
        if (gamepad == null)
        {
            gamepadPressed = false;
        }
        else
        {
            gamepadPressed = (Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed(0) && !x.synthetic)) ? true : false;
        }

        if ((gamepadPressed || Keyboard.current.anyKey.IsPressed(0)) && !FacilityManager.IsMenu)
        {
            StartCine.Stop();
            StartMenu.Play();
            FacilityManager.IsMenu = true;
        }
    }

    void OnYes()
    {
    }
}
