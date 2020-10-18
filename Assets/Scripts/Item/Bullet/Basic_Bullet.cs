using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Bullet : MonoBehaviour
{
    [Header("Setting")]
    public float DestroyAfterCollision;
    public GameObject CollisionEffect;
    public LayerMask TargetMask;
    public LayerMask PlayerMask;
    public float velocity = 0;
    public float damage = 0;
    public float CollisionDelay = 0;

    [Header("Physics")]
    [SerializeField]
    protected float StartSpeed = 0;
    [SerializeField]
    protected float AddSpeed = 0;
    protected Rigidbody rb;

    public AnimationCurve SpeedCurve;
    protected float time = 0;

    public float MaxDistnace = -1;
    public float MinSpeed = 0;
    protected bool IsEnable = true;
    
    List<PlayerHitten> playerHittens = new List<PlayerHitten>();
    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * StartSpeed);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        rb.AddForce(transform.forward * AddSpeed * SpeedCurve.Evaluate(time));
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (MaxDistnace > 0 && transform.localPosition.magnitude > MaxDistnace)
        {
            GameObject g = Instantiate(CollisionEffect, transform.position, transform.rotation);
            Destroy(g, 2f);
            Destroy(this.gameObject, DestroyAfterCollision);
            rb.isKinematic = true;
            IsEnable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (time < CollisionDelay) return;
        if (!IsEnable) return;
        if (!((TargetMask.value & 1 << other.gameObject.layer) > 0)) return;

        BulletHitInfo_AF bulletHitInfo = new BulletHitInfo_AF();

        if ((PlayerMask.value & 1 << other.gameObject.layer) > 0)
        {
            if (other.gameObject.transform.root.GetComponent<PlayerHitten>())
            {
                PlayerHitten hitten = other.gameObject.transform.root.GetComponent<PlayerHitten>();
                bulletHitInfo.hitTransform = other.transform;
                bulletHitInfo.bulletForce = (other.ClosestPoint(transform.position) - transform.position).normalized * velocity;
                // bulletHitInfo.hitNormal = raycastHit.normal;
                bulletHitInfo.hitPoint = other.ClosestPoint(transform.position);

                if (!playerHittens.Contains(hitten))
                {
                    hitten.OnDamaged(damage);
                }

                hitten.OnHit(bulletHitInfo);

                playerHittens.Add(hitten);
            }
        }
        GameObject g = Instantiate(CollisionEffect, transform.position, transform.rotation);
        Destroy(g, 2f);
        Destroy(this.gameObject, DestroyAfterCollision);
        rb.isKinematic = true;
        IsEnable = false;
    }
}
