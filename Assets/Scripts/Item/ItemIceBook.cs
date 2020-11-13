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
            List<Basic_Bullet> basic_Bullets = new List<Basic_Bullet>();
            basic_Bullets.Add(Fire(0));
            if (Enhaced)
            {
                basic_Bullets.Add(Fire(30));
                basic_Bullets.Add(Fire(-30));
            }

            if (basic_Bullets.Count > 1)
            {
                foreach (Basic_Bullet b in basic_Bullets)
                {
                    b.AddIgnore(basic_Bullets);
                }
            }
            
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

    Basic_Bullet Fire(float angle)
    {
        GameObject b = Instantiate(bullet, FollowTransform.GetComponent<PlayerItemHand>().way.position + 1.2f * FollowTransform.GetComponent<PlayerItemHand>().way.transform.forward, FollowTransform.GetComponent<PlayerItemHand>().way.rotation);
        b.transform.eulerAngles += new Vector3(0, angle, 0);
        Destroy(b, DestroyTime);
        if (b.GetComponent<Basic_Bullet>() != null)
        {
            b.GetComponent<Basic_Bullet>().PlayerID = PlayerID;
            return b.GetComponent<Basic_Bullet>();
        }
        return null;
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
