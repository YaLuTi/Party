using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiniPlayerControl : MonoBehaviour
{
    public int[] ClothNum = new int[2];

    [SerializeField]
    PlayerInput playerInput;

    public int Playernum;

    [SerializeField]
    Material[] materials;
    [SerializeField]
    SkinnedMeshRenderer BodyMeshRenderer1;

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] clips;

    InputDevice inputDevice;
    Gamepad gamePad;

    [SerializeField]
    float Now_a;
    [SerializeField]
    float a = 0.5f;

    float OffsetY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set(GameObject player, int num)
    {
        inputDevice = player.GetComponentInChildren<PlayerInput>().devices[0];
        foreach (Gamepad g in Gamepad.all)
        {
            if (g.name == inputDevice.name)
            {
                gamePad = g;
            }
        }
        OffsetY = transform.position.y;
        Now_a = -a;
        Material[] mats = BodyMeshRenderer1.materials;
        mats[0] = materials[num];
        BodyMeshRenderer1.materials = mats;
    }
}
