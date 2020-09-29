using System.Collections;
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
    // Start is called before the first frame update
    void Start()
    {
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
                    hitten.OnHit(bulletHitInfo);

                    if (!playerHittens.Contains(hitten))
                    {
                        hitten.OnDamaged(damage);
                    }

                    playerHittens.Add(hitten);
                }
                else if (collider.gameObject.transform.root.GetComponent <CreatureBasic>())
                {
                    Destroy(collider.gameObject);
                }
            }

            if(collider.gameObject.tag == "Item")
            {
                if (collider.gameObject.GetComponent<ItemBasic>())
                {
                    ItemBasic itemBasic = collider.gameObject.GetComponent<ItemBasic>();
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
