using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Game VFX")]
    public GameObject HitParticle;
    public GameObject PickUpParticle;

    

    public bool IsHolding = false;
    public bool IsThrowing = false;
    public bool IsThrowing2 = false;

    [Header("Game Value")]
    [SerializeField]
    float PickRadius;
    [SerializeField]
    float ThrowPower1 = 0.01f;
    [SerializeField]
    float ThrowPower2 = 0.01f;

    public float ThrowStrength = 0.1f;

    GameObject HighlightObject = null;

    public PlayerItemHand itemHand;
    PlayerStatusAnimator playerStatus;
    // Start is called before the first frame update

    private void Awake()
    {
        playerStatus = GetComponent<PlayerStatusAnimator>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(itemHand.HoldingItem == null)
        {
            IsThrowing = false;
            IsHolding = false;
            IsThrowing2 = false;
        }
        if (IsThrowing)
        {
            ThrowStrength += ThrowPower1 * Time.deltaTime;
            ThrowPower1 += ThrowPower2;
            ThrowStrength = Mathf.Min(ThrowStrength, 10f);
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, PickRadius);
        if (colliders.Length > 0)
        {
            float shortestDistance = 10;
            Collider pick = null;

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag != "Item") continue;
                if (collider.gameObject.GetComponent<ItemBasic>().IsHolded) continue;
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
                if (IsHolding) return;
                if(pick.gameObject == HighlightObject)
                {
                }
                else
                {
                    if(HighlightObject != null)
                    {
                        HighlightObject.GetComponent<ItemBasic>().CancelHighlight();
                    }
                    HighlightObject = pick.gameObject;
                    HighlightObject.GetComponent<ItemBasic>().PickHighlight();
                }
            }
            else
            {
                if (HighlightObject != null)
                {
                    HighlightObject.GetComponent<ItemBasic>().CancelHighlight();
                }
                HighlightObject = null;
            }
        }
    }

    void OnShoot()
    {
        if (IsHolding)
        {
            _playerItemStatus status = new _playerItemStatus();
            status.Throwing = IsThrowing2;
            string animation = itemHand.UseItem(status);
            if (animation == "Empty" || animation == "")
            {

            }
            else if(animation == "SetMine")
            {
                SetMine();
            }
            else
            {
                playerStatus.PlayerItemAnimation(animation);
            }
        }
    }

    void OnPick()
    {
        if (playerStatus.PlayerPick()) return;
        if (IsHolding && !IsThrowing2)
        {
            playerStatus.PlayerItem_Aim();
            IsThrowing = true;
            IsThrowing2 = true;
        }
        else if(!IsHolding)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, PickRadius);

            if (colliders.Length > 0)
            {
                float shortestDistance = 10;
                Collider pick = null;

                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.tag != "Item") continue;
                    if (collider.gameObject.GetComponent<ItemBasic>().IsHolded) continue;
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
            IsThrowing = false;
        }
    }

    void OnPause()
    {
        if (!enabled) return;
        StageController.Pause();
    }

    void ThrowItem()
    {
        itemHand.ThrowHoldingItem(ThrowStrength);
        Debug.Log("T");
        IsHolding = false;
        IsThrowing2 = false;
        ThrowStrength = 0.1f;
        ThrowPower1 = 0.01f;
    }

    void SetMine()
    {
        IsHolding = false;
        IsThrowing = false;
        IsThrowing2 = false;
        itemHand.ThrowHoldingItem(0);
    }

    public void OnHit(BulletHitInfo_AF info)
    {
        Destroy(Instantiate(HitParticle, info.hitPoint, Quaternion.identity), 2f);
        // GetComponent<Rigidbody>().AddForce(info.bulletForce);
        if (IsHolding)
        {
            itemHand.DropHoldingItem(info.bulletForce);
            IsHolding = false;
            IsThrowing = false;
            IsThrowing2 = false;
        }
    }
}

public class _playerItemStatus
{
    public bool Throwing;
}
