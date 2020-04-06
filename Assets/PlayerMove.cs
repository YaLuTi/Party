using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using System;

public class PlayerMove : MonoBehaviour
{
    
    [SerializeField]
    float PlayerMoveSpeed;
    [SerializeField]
    float ExplotionForce;
    [SerializeField]
    float ExplotionRadius;

    PlayerStatus playerStatus;

    bool MoveEnable = true;

    public GameObject Bullet;
    bool Fire;
    Rigidbody rb;

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
        playerStatus = GetComponent<PlayerStatus>();
        playerStatus.StatusUpdateHandler += OnStatusUpdate;
        rb = GetComponent<Rigidbody>();
    }

    /*
    void Shoot()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplotionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(ExplotionForce, transform.position, ExplotionRadius, 999.0F);
                rb.isKinematic = false;
            }
        }
        Debug.Log("Fire");
    }*/

    // Update is called once per frame
    void Update()
    {
        if (!MoveEnable) return;
        if (Fire)
        {
            Debug.Log("S");
        }

        // rb.velocity = new Vector3(h, 0, v) * 2.5f;

        transform.position += new Vector3(h, 0, v) * PlayerMoveSpeed * Time.deltaTime;

        if (Mathf.Abs(h) + Mathf.Abs(v) > 0.8f)
        {
            float angle = 0;
            angle = (Mathf.Atan2(v, h) * Mathf.Rad2Deg * -1);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, angle, 0), 180 * Time.deltaTime);
            
        }
        
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
