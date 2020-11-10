using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Staff : ItemBasic
{
    public GameObject bullet;
    public float DestroyTime;
    public Transform muzzle;
    [SerializeField]
    float BulletVelocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override string OnUse(_playerItemStatus status)
    {
        if (status.Throwing) return "Empty";
        return base.OnUse(status);
    }

    public override void OnTrigger()
    {
        base.OnTrigger();
        if (DurabilityCheck())
        {
            List<RFX4_PhysicsMotion> list = new List<RFX4_PhysicsMotion>();
            list.Add(Fire(0));
            if (Enhaced)
            {
                list.Add(Fire(30));
                list.Add(Fire(-30));
            }

            if (list.Count > 1)
            {
                foreach(RFX4_PhysicsMotion rFX4_Physics in list)
                {
                    rFX4_Physics.AddIgnore(list);
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

    RFX4_PhysicsMotion Fire(float angle)
    {
        GameObject b = Instantiate(bullet, FollowTransform.GetComponent<PlayerItemHand>().way.position + 1.2f * FollowTransform.GetComponent<PlayerItemHand>().way.transform.forward, FollowTransform.GetComponent<PlayerItemHand>().way.rotation);
        b.transform.eulerAngles += new Vector3(0, angle, 0);
        Destroy(b, DestroyTime);
        if (b.GetComponentInChildren<RFX4_PhysicsMotion>() != null)
        {
            b.GetComponentInChildren<RFX4_PhysicsMotion>().PlayerID = PlayerID;
            return b.GetComponentInChildren<RFX4_PhysicsMotion>();
        }
        if (b.GetComponentInChildren<Basic_Bullet>() != null)
        {
            b.GetComponentInChildren<Basic_Bullet>().PlayerID = PlayerID;
            return null;
        }
        return null;
    }

    void StaffFire()
    {
        Debug.Log("X");
    }
}
