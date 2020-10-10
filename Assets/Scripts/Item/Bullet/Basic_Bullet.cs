using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Bullet : MonoBehaviour
{
    [Header("Setting")]
    public float DestroyAfterCollision;
    public LayerMask TargetMask;

    [Header("Physics")]
    [SerializeField]
    float StartSpeed = 0;
    [SerializeField]
    float AddSpeed = 0;
    Rigidbody rb;

    public AnimationCurve SpeedCurve;
    float time = 0;

    public float MaxDistnace = -1;
    public float MinSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * StartSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * AddSpeed * SpeedCurve.Evaluate(time));
        Debug.Log(SpeedCurve.Evaluate(time));
        time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!((TargetMask.value & 1 << other.gameObject.layer) > 0)) return;
        Destroy(this.gameObject, DestroyAfterCollision);
        rb.isKinematic = true;
    }
}
