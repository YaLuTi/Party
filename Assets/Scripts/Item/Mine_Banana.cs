using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimFollow;

public class Mine_Banana : ItemMine
{
    public GameObject VFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Throw()
    {
        OnUse();
    }

    public override void SettedEvent()
    {
        base.SettedEvent();
        Collider[] rays = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < rays.Length; i++)
        {
            if (rays[i].transform.root.gameObject.tag == "Player")
            {
                if (rays[i].gameObject.transform.root.GetComponent<PlayerHitten>())
                {
                    BulletHitInfo_AF bulletHitInfo = new BulletHitInfo_AF();
                    bulletHitInfo.hitTransform = rays[i].transform;
                    bulletHitInfo.bulletForce = (rays[i].ClosestPoint(transform.position) - transform.position).normalized * 1000;
                    // bulletHitInfo.hitNormal = raycastHit.normal;
                    bulletHitInfo.hitPoint = rays[i].ClosestPoint(transform.position);
                    rays[i].gameObject.transform.root.GetComponent<PlayerHitten>().OnHit(bulletHitInfo);
                    Destroy(Instantiate(VFX, transform.position, Quaternion.identity), 3f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
