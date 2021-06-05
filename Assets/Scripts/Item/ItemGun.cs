using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGun : ItemBasic
{
    public GameObject bullet;
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
    {/*
        if (status.Throwing) return "Empty";
        if (DurabilityCheck())
        {
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            b.GetComponent<Rigidbody>().AddForce(BulletVelocity * transform.root.GetComponent<PlayerHitten>().FaceWay.forward);
            audioSource.PlayOneShot(UsingSound[0]);
            if(Durability == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
            return "Empty";
        }*/
        return base.OnUse(status);
    }

    public override void OnTrigger()
    {
        if (DurabilityCheck())
        {
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            b.GetComponent<Rigidbody>().AddForce(BulletVelocity * transform.root.GetComponent<PlayerHitten>().FaceWay.forward);
            audioSource.PlayOneShot(UsingSound[0]);
            if (Durability == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
        base.OnTrigger();
    }
}
