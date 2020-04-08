using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickItem : MonoBehaviour
{
    [Header("Game VFX")]
    public GameObject HitParticle;
    

    bool IsHolding = false;
    PlayerItemHand itemHand;
    // Start is called before the first frame update
    void Start()
    {
        itemHand = GetComponentInChildren<PlayerItemHand>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPick()
    {
        if (IsHolding)
        {
            itemHand.ThrowHoldingItem();
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
                }
            }
        }
    }

    public void OnHit(Vector3 p, Vector3 velocity)
    {
        Instantiate(HitParticle, p, Quaternion.identity);
        GetComponent<Rigidbody>().AddForce(velocity * 20);

    }
}
