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
            GameObject b = Instantiate(bullet, FollowTransform.GetComponent<PlayerItemHand>().way.position + 1.2f * FollowTransform.GetComponent<PlayerItemHand>().way.transform.forward, FollowTransform.GetComponent<PlayerItemHand>().way.rotation);
            Destroy(b, DestroyTime);
            
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


    void StaffFire()
    {
        Debug.Log("X");
    }
}
