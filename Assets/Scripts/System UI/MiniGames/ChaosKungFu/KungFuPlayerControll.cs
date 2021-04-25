using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KungFuPlayerControll : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;

    InputDevice inputDevice;
    Gamepad gamePad;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Set(GameObject player)
    {
        inputDevice = player.GetComponentInChildren<PlayerInput>().devices[0];
        foreach(Gamepad g in Gamepad.all)
        {
            if(g.name == inputDevice.name)
            {
                gamePad = g;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDevice == null) return;

        if (gamePad.buttonNorth.IsPressed(0))
        {
            Debug.Log("T");
        }
    }
}
