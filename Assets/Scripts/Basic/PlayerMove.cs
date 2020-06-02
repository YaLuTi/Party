using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;
using AnimFollow;

public class PlayerMove : MonoBehaviour
{
    [Header("Game Value")]
    [SerializeField]
    float PlayerMoveSpeed; // Set by user
    [SerializeField]
    Vector3 spawnPosition;

    [Header("Game SFX")]
    public AudioClip[] stepClips;
    AudioSource audioSource;

    [Header("Game VFX")]
    [SerializeField]
    GameObject StepParticlePosition;
    [SerializeField]
    GameObject StepParticle;
    float StepCooldownValue = 12; // Step Particle Cooldown
    float StepCooldown = 0;
    
    Animator anim;          // Reference to the animator component.
    HashIDs_AF hash;            // Reference to the HashIDs.

    public float animatorSpeed = 1.3f; // Read by RagdollControl
    public float speedDampTime = .1f;   // The damping for the speed parameter
    float mouseInput;
    public float mouseSensitivityX = 100f;
    public bool inhibitMove = false; // Set from RagdollControl
    public Vector3 glideFree = Vector3.zero; // Set from RagdollControl
    Vector3 glideFree2 = Vector3.zero;
    [HideInInspector] public bool inhibitRun = false; // Set from RagdollControl

    Rigidbody[] rbs; // 有重複的參數在PlayerIdentity被取得 之後要修
    public Transform rig;

    bool MoveEnable = true;

    float MoveMultiplier = 0;

    PlayerStatusAnimator playerStatus;
    float h;
    float v;

    PlayerControls inputActions;
    // Start is called before the first frame update

    private void Awake()
    {
        inputActions = new PlayerControls();
    }
    

    void Start()
    {
        playerStatus = GetComponent<PlayerStatusAnimator>();
        playerStatus.StatusUpdateHandler += OnStatusUpdate;
        audioSource = GetComponent<AudioSource>();
        rbs = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rig.position.y < -5 || rig.position.y > 15)
        {
            StartCoroutine(ReSpawn());
        }
        if (inhibitMove) return;

        if (MoveEnable)
        {
            MoveMultiplier = 0;
        }
        else
        {
            MoveMultiplier = -1f;
        }

        // rb.velocity = new Vector3(h, 0, v) * 2.5f;

        glideFree2 = Vector3.Lerp(glideFree2, glideFree, .01f);
        transform.position += new Vector3(h, 0, v) * PlayerMoveSpeed * (1 + MoveMultiplier) * Time.deltaTime + (glideFree * Time.deltaTime);

        // 暫時將AnimatorSpeed的Update寫成二分法 之後將Smooth轉向引入後再改成數學判斷式
        if (Mathf.Abs(h) + Mathf.Abs(v) > 0.8f)
        {
            float angle = 0;
            angle = (Mathf.Atan2(-h, v) * Mathf.Rad2Deg * -1);

            playerStatus.MoveSpeedUpdate(1);
            
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angle, 0), 480 * Time.deltaTime);
            
            SpawnStepParticle();
        }
        else
        {
            playerStatus.MoveSpeedUpdate(0);
        }
        
    }

    // 亂寫的
    IEnumerator ReSpawn()
    {
        BulletHitInfo_AF bulletHitInfo_AF = new BulletHitInfo_AF();
        transform.root.GetComponent<PlayerHitten>().OnHit(bulletHitInfo_AF);
        transform.root.GetComponent<PlayerIdentity>().Respawn();
        yield return null;
    }

    void SpawnStepParticle()
    {
        if (StepParticle == null) return;
        if(StepCooldown < StepCooldownValue)
        {
            StepCooldown++;
            return;
        }
        StepCooldown = 0;

        GameObject g = Instantiate(StepParticle, StepParticlePosition.transform.position, Quaternion.identity);
        int r = UnityEngine.Random.Range(0, stepClips.Length);
        audioSource.PlayOneShot(stepClips[r]);
        Destroy(g, 1);
    }

    void OnMove(InputValue value)
    {
        h = value.Get<Vector2>().x;
        v = value.Get<Vector2>().y;
    }

    void OnPick()
    {
        if (inhibitMove)
        {
            anim.SetFloat("GetUpSpeedMultiplier", anim.GetFloat("GetUpSpeedMultiplier") + 0.2f);
        }
    }
    void OnShoot()
    {
        if (inhibitMove)
        {
            anim.SetFloat("GetUpSpeedMultiplier", anim.GetFloat("GetUpSpeedMultiplier") + 0.2f);
        }
    }

    public void SetMoveEnable(bool b)
    {
        MoveEnable = b;
    }

    public void Hitten()
    {
        inhibitMove = true;
        anim.SetFloat("GetUpSpeedMultiplier", 1);
    }

    void OnStatusUpdate(object sender, StatusEventArgs args)
    {
        Debug.Log("F");
    }

    private void OnEnable()

    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
