using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class JumpOpen : MonoBehaviour
{
    bool IsUse = false;
    public PlayableDirector playableDirector;
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
            gamepadPressed = Gamepad.current.startButton.isPressed ? true : false;
        }

        if ((gamepadPressed && !IsUse) || (Keyboard.current.escapeKey.IsPressed(0) && !IsUse))
        {
            playableDirector.Play();
            IsUse = true;
        }
    }
}
