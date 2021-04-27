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
    float JumpcooldownSpeed = 2;

    float Jumpcooldown = 0;
    float JumpTime;

    [SerializeField]
    GameObject Shield;
    public bool IsInvisable;


    [SerializeField]
    float ShieldExistTime = 0.3f;
    [SerializeField]
    float ShieldcooldownSpeed = 0.5f;

    float Shieldcooldown = 0;
    float ShieldTime;

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

        if (gamePad.buttonSouth.IsPressed(0) && Jumpcooldown == 0)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Jumping", true);
            Now_a = a;
            Jumpcooldown = -1;
        }
        if(Now_a > -a)
        {
            transform.position += new Vector3(0, Now_a, 0) * Time.deltaTime * (Mathf.Abs(Now_a) / 2);
            Now_a -= s * Time.deltaTime;
            if (-a - Now_a >= (-s * Time.deltaTime))
            {
                animator.SetBool("Jumping", false);
                transform.DOMoveY(OffsetY, 0.05f);
                Jumpcooldown = 1;
                Now_a = -a;
            }
        }
        if(Jumpcooldown > 0)
        {
            Jumpcooldown = Mathf.Lerp(1, 0, JumpTime);
            JumpTime += JumpcooldownSpeed * Time.deltaTime;
        }
        else
        {
            JumpTime = 0;
        }

        if(gamePad.buttonWest.IsPressed(0) && Shieldcooldown == 0)
        {
            Shieldcooldown = 1;
            StartCoroutine(ShieldEvent());
        }
    }

    IEnumerator ShieldEvent()
    {
        Shield.SetActive(true);
        IsInvisable = true;
        yield return new WaitForSeconds(ShieldExistTime);
        Shield.SetActive(false);
        IsInvisable = false;
        yield return new WaitForSeconds(ShieldcooldownSpeed);
        Shieldcooldown = 0;
        yield return null;
    }
}
