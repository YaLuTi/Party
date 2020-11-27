using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TimelineHelper : MonoBehaviour
{
    bool Ready = false;
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

        if (gamepad.startButton.wasPressedThisFrame && !Ready)
        {
            Ready = true;
            playableDirector.Play();
        }
    }

    public void PlayBGM()
    {
        StageManager.PlayBGM();
    }
}
