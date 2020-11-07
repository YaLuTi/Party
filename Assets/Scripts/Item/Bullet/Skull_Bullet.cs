using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull_Bullet : Basic_Bullet
{
    public int Durability = 3;
    float cooldown = 10;

    public override void Start()
    {
        base.Start();
        rb.AddForce(new Vector3(0, 500, 0));
    }

    public override void Update()
    {
        base.Update();
        cooldown++;
        rb.AddForce(new Vector3(0, -1, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (time < CollisionDelay) return;
        if (cooldown < 10) return;
        if (!IsEnable) return;
        if (!((TargetMask.value & 1 << collision.gameObject.layer) > 0)) return;

        if ((PlayerMask.value & 1 << collision.gameObject.layer) > 0)
        {
            if (collision.gameObject.transform.root.GetComponent<PlayerHitten>())
            {
            }
        }
        cooldown = 0;
        GameObject g = Instantiate(CollisionEffect, transform.position, Quaternion.Euler(collision.transform.eulerAngles + new Vector3(-90,0,0)));
        g.GetComponentInChildren<BasicExplosion>().PlayerID = PlayerID;
        Durability--;
        if(Durability <= 0)
        {
            Destroy(g, 2f);
            Destroy(this.gameObject, DestroyAfterCollision);
            rb.isKinematic = true;
            IsEnable = false;
        }
    }
}
