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
        bulletHitInfo.damage = damage;
        foreach (Collider collider in colliders)
        {
            if ((layerMask.value & 1 << collider.gameObject.layer) > 0)
            {
                if (collider.gameObject.transform.root.GetComponent<PlayerHitten>())
                {
                    bulletHitInfo.hitTransform = collider.transform;
                    bulletHitInfo.bulletForce = (collider.ClosestPoint(transform.position) - transform.position).normalized * velocity;
                    // bulletHitInfo.hitNormal = raycastHit.normal;
                    bulletHitInfo.hitPoint = collider.ClosestPoint(transform.position);
                    collider.gameObject.transform.root.GetComponent<PlayerHitten>().OnHit(bulletHitInfo);
                }else if (collider.gameObject.transform.root.GetComponent <CreatureBasic>())
                {
                    Destroy(collider.gameObject);
                }
            }

            if(collider.gameObject.tag == "Item")
            {
                if (collider.gameObject.transform.root.GetComponent<ItemBasic>())
                {
                    bulletHitInfo.hitTransform = collider.transform;
                    bulletHitInfo.bulletForce = (collider.ClosestPoint(transform.position) - transform.position).normalized * velocity;
                    bulletHitInfo.hitPoint = collider.ClosestPoint(transform.position);
                    collider.gameObject.transform.root.GetComponent<ItemBasic>().AddForce(bulletHitInfo);
                }
            }
        }
        yield return null;
    }
}
