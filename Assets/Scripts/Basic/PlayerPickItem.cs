using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickItem : MonoBehaviour
{
    [Header("Game VFX")]
    public GameObject HitParticle;
    public GameObject PickUpParticle;
    

    bool IsHolding = false;
    PlayerItemHand itemHand;
    PlayerStatusAnimator playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        itemHand = GetComponentInChildren<PlayerItemHand>();
        playerStatus = GetComponent<PlayerStatusAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnShoot()
    {
        if (IsHolding)
        {
            itemHand.UseItem();
        }
    }

    void OnPick()
    {
        if (IsHolding)
        {
            playerStatus.PlayerItem_Throw();
            IsHolding = false;
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

            if (colliders.Length > 0)
            {
                float shortestDistance = 10;
                Collider pick = null;

                foreach (Collider collider in colliders)
                {
                    Debug.Log("C");
                    if (collider.gameObject.tag != "Item") continue;
                    if (pick == null)
                    {
                        Debug.Log("D");
                        pick = collider;
                        shortestDistance = Vector3.Distance(this.gameObject.transform.position, pick.transform.position);
                    }
                    float distance = Vector3.Distance(this.gameObject.transform.position, pick.transform.position);
                    if (distance < shortestDistance)
                    {
                        pick = collider;
                        shortestDistance = distance;
                    }
                }

                if (pick != null)
                {
                    IsHolding = true;
                    itemHand.SetHoldingItem(pick.gameObject);
                    GameObject g = Instantiate(PickUpParticle, transform.position, Quaternion.identity);
                    g.transform.parent = this.gameObject.transform;
                    Destroy(g, 1);
                }
            }
        }
    }

    void Throw()
    {
        itemHand.ThrowHoldingItem();
    }

    public void OnHit(Vector3 p, Vector3 velocity)
    {
        Instantiate(HitParticle, p, Quaternion.identity);
        GetComponent<Rigidbody>().AddForce(velocity * 20);
        if (IsHolding)
        {
            itemHand.DropHoldingItem(-velocity);
            IsHolding = false;
        }
    }
}
