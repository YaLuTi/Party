using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickItem : MonoBehaviour
{
    [Header("Game VFX")]
    public GameObject HitParticle;
    public GameObject PickUpParticle;

    

    bool IsHolding = false;
    bool IsThrowing = false;

    [Header("Game Value")]
    [SerializeField]
    float PickRadius;
    [SerializeField]
    float ThrowPower1 = 0.01f;
    [SerializeField]
    float ThrowPower2 = 0.01f;

    public float ThrowStrength = 0.1f;

    public PlayerItemHand itemHand;
    PlayerStatusAnimator playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponent<PlayerStatusAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsThrowing)
        {
            ThrowStrength += ThrowPower1 * Time.deltaTime;
            ThrowPower1 += ThrowPower2;
            ThrowStrength = Mathf.Min(ThrowStrength, 10f);
        }
    }

    void OnShoot()
    {
        if (IsHolding)
        {
            string animation = itemHand.UseItem();
            if (animation == "Empty" || animation == "")
            {

            }
            else
            {
                playerStatus.PlayerItemAnimation(animation);
            }
        }
    }

    void OnPick()
    {
        if (IsHolding)
        {
            playerStatus.PlayerItem_Aim();
            IsThrowing = true;
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, PickRadius);

            if (colliders.Length > 0)
            {
                float shortestDistance = 10;
                Collider pick = null;

                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag != "Item") continue;
                    if (pick == null)
                    {
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

    void OnThrow()
    {
        if (IsThrowing)
        {
            playerStatus.PlayerItem_Throw();
            IsHolding = false;
        }
    }

    void ThrowItem()
    {
        itemHand.ThrowHoldingItem(ThrowStrength);
        IsThrowing = false;
        ThrowStrength = 0.1f;
        ThrowPower1 = 0.01f;
    }

    void SetMine()
    {
        IsHolding = false;
        itemHand.ThrowHoldingItem(0);
    }

    public void OnHit(BulletHitInfo_AF info)
    {
        Instantiate(HitParticle, info.hitPoint, Quaternion.identity);
        // GetComponent<Rigidbody>().AddForce(info.bulletForce);
        if (IsHolding)
        {
            itemHand.DropHoldingItem(info.bulletForce);
            IsHolding = false;
            IsThrowing = false;
        }
    }
}
