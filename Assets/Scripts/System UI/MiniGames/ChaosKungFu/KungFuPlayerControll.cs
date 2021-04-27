using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class KungFuPlayerControll : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;

    InputDevice inputDevice;
    Gamepad gamePad;

    Animator animator;

    [SerializeField]
    float a = 0.5f;

    [SerializeField]
    float s = 0.01f;

    [SerializeField]
    float Now_a;

    float OffsetY;

    [SerializeField]
    float cooldownSpeed = 2;

    float cooldown = 0;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
        OffsetY = transform.position.y;
        Now_a = -a;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePad == null) return;

        if (gamePad.buttonSouth.IsPressed(0) && cooldown == 0)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Jumping", true);
            Now_a = a;
            cooldown = -1;
        }
        if(Now_a > -a)
        {
            transform.position += new Vector3(0, Now_a, 0) * Time.deltaTime * (Mathf.Abs(Now_a) / 2);
            Now_a -= s;
            if (-a - Now_a >= -s)
            {
                animator.SetBool("Jumping", false);
                transform.DOMoveY(OffsetY, 0.05f);
                cooldown = 1;
                Now_a = -a;
            }
        }
        if(cooldown > 0)
        {
            cooldown = Mathf.Lerp(1, 0, t);
            t += cooldownSpeed * Time.deltaTime;
        }
        else
        {
            t = 0;
        }

    }
}
