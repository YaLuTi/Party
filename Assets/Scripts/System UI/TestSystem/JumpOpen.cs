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
        if (gamepad == null)
            return; // No gamepad connected.

        if ((Gamepad.current.startButton.isPressed && !IsUse))
        {
            playableDirector.Play();
            IsUse = true;
        }
    }
}
