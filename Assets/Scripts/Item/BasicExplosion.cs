﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimFollow;

public class BasicExplosion : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float radius;
    [SerializeField]
    float delay;
    [SerializeField]
    float velocity;
    [SerializeField]
    LayerMask layerMask;

    List<PlayerHitten> playerHittens = new List<PlayerHitten>();
    public int PlayerID;

    // Start is called before the first frame update
    void Start()
    {
        radius = radius * transform.localScale.x;
        StartCoroutine(explosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator explosion()
    {
        yield return new WaitForSecondsRealtime(delay);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        BulletHitInfo_AF bulletHitInfo = new BulletHitInfo_AF();
        foreach (Collider collider in colliders)
        {
            if ((layerMask.value & 1 << collider.gameObject.layer) > 0)
            {
                if (collider.gameObject.transform.root.GetComponent<PlayerHitten>())
                {
                    PlayerHitten hitten = collider.gameObject.transform.root.GetComponent<PlayerHitten>();
                    bulletHitInfo.hitTransform = collider.transform;
                    bulletHitInfo.bulletForce = (collider.ClosestPoint(transform.position) - transform.position).normalized * velocity;
                    // bulletHitInfo.hitNormal = raycastHit.normal;
                    bulletHitInfo.hitPoint = collider.ClosestPoint(transform.position);

                    if (!playerHittens.Contains(hitten))
                    {
                        hitten.OnDamaged(damage, PlayerID);
                    }

                    hitten.OnHit(bulletHitInfo);
                    
                    playerHittens.Add(hitten);
                }
                else if (collider.gameObject.transform.root.GetComponent <CreatureBasic>())
                {
                    collider.gameObject.transform.root.GetComponent<CreatureBasic>().Death();
                    collider.GetComponent<Rigidbody>().AddForceAtPosition((collider.ClosestPoint(transform.position) - transform.position).normalized * velocity * 2, collider.ClosestPoint(transform.position));
                }
            }

            if(collider.gameObject.tag == "Item")
            {
                Debug.Log(collider.gameObject.name);
                if (collider.gameObject.GetComponent<ItemBasic>())
                {
                    ItemBasic itemBasic = collider.gameObject.GetComponent<ItemBasic>();

                    // 記得取消
                    // itemBasic.OnUse();

                    if (itemBasic.IsHolded) continue;
                    bulletHitInfo.hitTransform = collider.transform;
                    bulletHitInfo.bulletForce = (collider.ClosestPoint(transform.position) - transform.position).normalized * velocity;
                    bulletHitInfo.hitPoint = collider.ClosestPoint(transform.position);
                    itemBasic.AddForce(bulletHitInfo);
                }
            }
        }
        yield return null;
    }
}
