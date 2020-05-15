using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimFollow;

public class ItemBasic : MonoBehaviour
{
    Rigidbody rb;
    bool IsThrowing = false;
    public bool IsHolded = false;
    int PlayerID = -1;

    [Header("GameValue")]
    [SerializeField]
    public float Durability = 1;
    [SerializeField]
    Vector3 HoldedPosition;
    [SerializeField]
    Vector3 HoldedRotation;

    public string animation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual string OnUse()
    {
        return animation;
    }

    public void Hold()
    {
        transform.localPosition = HoldedPosition;
        transform.localRotation = Quaternion.Euler(HoldedRotation);
        IsHolded = true;
    }

    public bool DurabilityCheck()
    {
        if (Durability > 0)
        {
            Durability--;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddForce(BulletHitInfo_AF bulletHitInfo)
    {
        bulletHitInfo.hitTransform.GetComponent<Rigidbody>().AddForceAtPosition(bulletHitInfo.bulletForce, bulletHitInfo.hitPoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
