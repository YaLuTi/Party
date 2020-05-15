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
    void Update()
    {
        
    }

    public override string OnUse()
    {
        if (DurabilityCheck())
        {
            GameObject b = Instantiate(bullet, muzzle.position, muzzle.rotation);
            b.GetComponent<Rigidbody>().AddForce(BulletVelocity * transform.root.GetComponent<PlayerHitten>().FaceWay.forward);
        }
        else
        {
            return "Empty";
        }
        return base.OnUse();
    }
}
