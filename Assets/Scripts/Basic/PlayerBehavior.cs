using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [Header("Game VFX")]
    // public GameObject HitParticle;
    public GameObject PickUpParticle;
    [Header("Game SFX")]
    [SerializeField]
    AudioClip[] SpellSounds;
    AudioSource audioSource;



    public bool IsHolding = false;
    public bool IsCharging = false;
    public bool IsThrowing = false;
    public bool IsThrowing2 = false;

    [Header("Game Value")]
    [SerializeField]
    float PickRadius;
    [SerializeField]
    float ThrowPower1 = 0.01f;
    [SerializeField]
    float ThrowPower2 = 0.01f;

    public float ThrowStrength = 6f;

    GameObject HighlightObject = null;

    public PlayerItemHand itemHand;
    PlayerStatusAnimator playerStatus;

    GameObject ChoosingFacility;

    public delegate void PlayerPickHandler(ItemBasic item);
    public event PlayerPickHandler PickEvent;
    public delegate void PlayerChargeHandler(float v);
    public event PlayerChargeHandler ChargeEvent;
    // Start is called before the first frame update

    private void Awake()
    {
        playerStatus = GetComponent<PlayerStatusAnimator>();
        audioSource = GetComponent<AudioSource>();
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
            ThrowStrength = Mathf.Min(ThrowStrength, 13f);
            ChargeEvent?.Invoke(ThrowStrength / 13f);
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
        if (colliders.Length > 0)
        {
            float shortestDistance = 10;
            Collider pick = null;

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.tag != "SceneItem") continue;
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
                if (pick.gameObject != ChoosingFacility)
                {
                    ChoosingFacility = pick.gameObject;
                    ChoosingFacility.GetComponent<FacilityArea>().PlayersNum++;
                }
            }
            else
            {
                if(ChoosingFacility != null)
                {
                    ChoosingFacility.GetComponent<FacilityArea>().PlayersNum--;
                    ChoosingFacility = null;
                }
            }
        }
        else
        {
            if(ChoosingFacility != null)
            {
                ChoosingFacility.GetComponent<FacilityArea>().PlayersNum--;
                ChoosingFacility = null;
            }
        }
    }


    void OnRelease()
    {
        if (!this.enabled) return;
        if (playerStatus.PlayerPick()) return;
        if (IsHolding)
        {
            if (IsHolding)
            {
                _playerItemStatus status = new _playerItemStatus();
                status.Throwing = IsThrowing2;
                string animation = itemHand.ReleaseItem(status);
                if (animation == "Empty" || animation == "")
                {

                }
                else if (animation == "SetMine")
                {
                    SetMine();
                }
                else
                {
                    if (IsThrowing) return;
                    if (IsCharging)
                    {
                        IsCharging = false;
                        playerStatus.PlayerItemAnimation("_Interupt");
                        playerStatus.PlayerItemAnimation(animation);
                    }
                }
            }
        }
    }

    // Pick & Shoot
    void OnPick()
    {
        if (!this.enabled) return;
        if (playerStatus.PlayerPick()) return;
        if (IsHolding)
        {
            if (IsHolding && playerStatus.CanAnimation())
            {
                _playerItemStatus status = new _playerItemStatus();
                status.Throwing = IsThrowing2;
                string animation = itemHand.UseItem(status);
                if (animation == "Empty" || animation == "")
                {

                }
                else if (animation == "SetMine")
                {
                    SetMine();
                }
                else if(animation == "Axe")
                {
                    AnimatorClipInfo[] CurrentClipInfo;
                    CurrentClipInfo = playerStatus.animator.GetCurrentAnimatorClipInfo(1);
                    if (CurrentClipInfo[0].clip.name == "Twinblades_attack02_Inplace")
                    {

                    }
                    else
                    {
                        playerStatus.PlayerItemAnimation(animation);
                    }
                }
                else
                {
                    if (IsThrowing) return;
                    if (animation[0] == '_')
                    {
                        IsCharging = true;
                        playerStatus.PlayerItemAnimationBool(animation, true);
                    }
                    else
                    {
                        playerStatus.PlayerItemAnimation(animation);
                    }
                }
            }
            /*playerStatus.PlayerItem_Aim();
            IsThrowing = true;
            IsThrowing2 = true;*/
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
                    PickEvent?.Invoke(pick.gameObject.GetComponent<ItemBasic>());
                    if (pick.gameObject.GetComponent<ItemBasic>().IdleAnimation != "")
                    {
                        playerStatus.PlayerItemAnimationBool(pick.gameObject.GetComponent<ItemBasic>().IdleAnimation, true);
                    }
                    GameObject g = Instantiate(PickUpParticle, transform.position, Quaternion.identity);
                    g.transform.parent = this.gameObject.transform;
                    Destroy(g, 1);
                }
            }
        }
    }

    void OnInteract()
    {
        if(ChoosingFacility != null)
        {
            ChoosingFacility.GetComponent<FacilityArea>().OnUse(this);
        }
    }
    
    // Throw
    void OnThrow()
    {
        if (IsThrowing)
        {
            if (itemHand.HoldingItem.gameObject.GetComponent<ItemBasic>().IdleAnimation != "")
            {
                playerStatus.PlayerItemAnimationBool(itemHand.HoldingItem.gameObject.GetComponent<ItemBasic>().IdleAnimation, false);
            }
            playerStatus.PlayerItem_Throw();
            IsThrowing = false;
            IsCharging = false;
        }
    }

    // Throw
    void OnShoot()
    {
        if (playerStatus.PlayerPick() || !playerStatus.CanAnimation() || IsCharging) return;
        if (IsHolding && !IsThrowing2)
        {
            playerStatus.PlayerItem_Aim();
            IsThrowing = true;
            IsThrowing2 = true;
            IsCharging = true;
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
        IsHolding = false;
        IsThrowing2 = false;
        ThrowStrength = 3f;
        ThrowPower1 = 0.01f;
    }

    void SetMine()
    {
        IsHolding = false;
        IsThrowing = false;
        IsThrowing2 = false;
        itemHand.ThrowHoldingItem(0);
    }

    void TriggerItem()
    {
        int r = Random.Range(0, SpellSounds.Length);
        audioSource.PlayOneShot(SpellSounds[r], 1f);
        itemHand.TriggerItem();
    }

    void ItemEnd()
    {
        itemHand.ItemEnd();
    }

    public void OnHit(BulletHitInfo_AF info)
    {
        // GetComponent<Rigidbody>().AddForce(info.bulletForce);
        ThrowStrength = 3f;
        ThrowPower1 = 0.01f;

        if (IsCharging)
        {
            playerStatus.PlayerItemAnimation("_Interupt");
            IsCharging = false;
        }
        if (IsThrowing)
        {
            playerStatus.CancelThrow();
        }
        if (IsHolding)
        {
            itemHand.DropHoldingItem(info.bulletForce / 10);
            IsHolding = false;
            IsThrowing = false;
            IsThrowing2 = false;
        }
    }
    public void OnHit()
    {
        // GetComponent<Rigidbody>().AddForce(info.bulletForce);

        if (IsCharging)
        {
            playerStatus.PlayerItemAnimation("_Interupt");
            IsCharging = false;
        }
        if (IsThrowing)
        {
            playerStatus.CancelThrow();
        }
        if (IsHolding)
        {
            itemHand.DropHoldingItem();
            ThrowStrength = 3f;
            ThrowPower1 = 0.01f;
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
