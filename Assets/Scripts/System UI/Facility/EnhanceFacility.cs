using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnhanceFacility : FacilityArea
{
    public ItemBasic item;
    public Vector3 p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        base.OnUse(playerBehavior);
        if (playerBehavior.IsHolding)
        {
            GameObject g = playerBehavior.itemHand.HoldingItem;
            playerBehavior.OnHit();
            
            g.GetComponent<Collider>().isTrigger = true;
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            g.GetComponent<ItemBasic>().Put(this.transform);
            StartCoroutine(_PutAnimation(g));
        }
    }

    IEnumerator _PutAnimation(GameObject g)
    {
        // g.transform.DOMoveY(g.transform.position.y + 0.5f, 0.6f).SetEase(Ease.OutQuint);
        g.transform.DOLocalMove(p, 2f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(0.4f);

        g.transform.DOMoveY(g.transform.position.y + 1, 2).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(2);

        g.GetComponent<Collider>().isTrigger = false;
        Rigidbody rb = g.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        g.transform.parent = null;
        g.GetComponent<ItemBasic>().IsHolded = false;
        g.GetComponent<ItemBasic>().Enhance();
        yield return null;
    }
}
