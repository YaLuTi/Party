using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIceBook : Item_Staff
{
    public float charge = 3;
    public float MaxCharge = 5;
    public float ChargeSpeed = 3;
    bool IsCharging = false;

    float cooldown = 0;
    float cooldownvalue = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Update()
    {
        base.Update();
        if (IsCharging && charge < MaxCharge) charge += Time.deltaTime * ChargeSpeed;
    }

    public override void OnTrigger()
    {
        if (DurabilityCheck())
        {
            GameObject b = Instantiate(bullet, FollowTransform.GetComponent<PlayerItemHand>().way.position + 1.2f * FollowTransform.GetComponent<PlayerItemHand>().way.transform.forward, FollowTransform.GetComponent<PlayerItemHand>().way.rotation);
            Destroy(b, DestroyTime);
            b.GetComponentInChildren<Basic_Bullet>().PlayerID = PlayerID;
            if (Durability == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public override string OnUse(_playerItemStatus status)
    {
        if (status.Throwing) return "Empty";
        return base.OnUse(status);
    }

    /*public override string OnRelease(_playerItemStatus status)
    {
        IsCharging = false;
        if (charge == 1) return "";
        return base.OnRelease(status);
    }

    public override void Throw()
    {
        base.Throw();
        IsCharging = false;
        charge = 3;
    }*/
}
