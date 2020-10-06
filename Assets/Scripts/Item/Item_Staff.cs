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
            GameObject b = Instantiate(bullet, FollowTransform.GetComponent<PlayerItemHand>().way.position + new Vector3(0,1,0) + -1.5f * transform.forward, FollowTransform.GetComponent<PlayerItemHand>().way.rotation);
            Destroy(b, DestroyTime);
            // b.GetComponent<Rigidbody>().AddForce(BulletVelocity * transform.root.GetComponent<PlayerHitten>().FaceWay.forward);
            // audioSource.PlayOneShot(UsingSound[0]);
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
