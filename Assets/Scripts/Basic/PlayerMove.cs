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
    [HideInInspector] public Vector3 glideFree = Vector3.zero; // Set from RagdollControl
    Vector3 glideFree2 = Vector3.zero;
    [HideInInspector] public bool inhibitRun = false; // Set from RagdollControl

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!MoveEnable) return;

        // rb.velocity = new Vector3(h, 0, v) * 2.5f;

        transform.position += new Vector3(h, 0, v) * PlayerMoveSpeed * (1 - MoveMultiplier) * Time.deltaTime;

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
        Destroy(g, 1);
    }

    void OnMove(InputValue value)
    {
        h = value.Get<Vector2>().x;
        v = value.Get<Vector2>().y;
    }

    public void SetMoveEnable(bool b)
    {
        MoveEnable = b;
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
