using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimelineJump : MonoBehaviour
{
    bool IsUse = false;
    public PlayableDirector playableDirector;
    [SerializeField]
    float enableTime;
    [SerializeField]
    bool CanJump = true;
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

        if ((gamepadPressed && !IsUse) || (Keyboard.current.escapeKey.IsPressed(0) && !IsUse) && playableDirector.time > 0.5f && playableDirector.time < enableTime && CanJump)
        {
            playableDirector.time = enableTime;
            IsUse = true;
        }
    }

    public void ThrowPlayer()
    {
        SceneChangeTest sceneChangeTest =  GameObject.FindGameObjectWithTag("SceneChangeTester").GetComponent<SceneChangeTest>();
        sceneChangeTest.ThrowPlayer();
    }
}
