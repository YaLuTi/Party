using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class KungFuPlayerControll : MonoBehaviour
{
    [SerializeField]
    PlayerInput playerInput;

    public int[] ClothNum = new int[2];
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

    [SerializeField]
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

    bool IsDeath = false;
    [SerializeField]
    float JumpDelay = -3;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Set(GameObject player, int num)
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
        Material[] mats = BodyMeshRenderer1.materials;
        mats[0] = materials[num];
        BodyMeshRenderer1.materials = mats;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDeath) return;
        if (gamePad == null) return;

        if (gamePad.buttonSouth.IsPressed(0) && Jumpcooldown == 0 && Shieldcooldown == 0)
        {
            animator.SetTrigger("Jump");
            animator.SetBool("Jumping", true);
            Jump();
            Now_a = a;
            audioSource.PlayOneShot(clips[1]);
            Jumpcooldown = -1;
        }
        if(Now_a > -a)
        {
            transform.position += new Vector3(0, Now_a, 0) * Time.deltaTime * (Mathf.Abs(Now_a) / 2);
            Now_a -= s * Time.deltaTime;
            if(Now_a < 0)
            {
                Now_a -= 3 * Time.deltaTime;
            }
            // if (-a - Now_a >= ((-s - 3) * Time.deltaTime))
            if(Now_a < JumpDelay)
            {
                animator.SetBool("Jumping", false);
            }
            if (-a - Now_a >= ((-s - 3) * Time.deltaTime))
            {
                transform.DOMoveY(OffsetY, Time.deltaTime);
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

        if(gamePad.buttonWest.IsPressed(0) && Jumpcooldown == 0 && Shieldcooldown == 0)
        {
            Shieldcooldown = 1;
            Swing();
            audioSource.PlayOneShot(clips[0]);
            StartCoroutine(ShieldEvent());
        }
    }

    public delegate void Handler(int num);
    public Handler OnSwing;
    public void Swing()
    {
        OnSwing?.Invoke(Playernum);
    }

    public Handler OnJump;
    public void Jump()
    {
        OnJump?.Invoke(Playernum);
    }

    public void Death()
    {
        if (IsDeath) return;
        IsDeath = true;
        audioSource.PlayOneShot(clips[2], 0.4f);
        audioSource.PlayOneShot(clips[3], 0.4f);
        KungFuManager.PlayerDeath();
    }

    IEnumerator ShieldEvent()
    {
        Shield.SetActive(true);
        IsInvisable = true;
        animator.SetTrigger("Swing");
        yield return new WaitForSeconds(ShieldExistTime);
        Shield.SetActive(false);
        IsInvisable = false;
        yield return new WaitForSeconds(ShieldcooldownSpeed);
        Shieldcooldown = 0;
        yield return null;
    }
}
