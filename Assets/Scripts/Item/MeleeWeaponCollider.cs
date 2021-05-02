using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponCollider : MonoBehaviour
{
    public int PlayerID;
    public List<PlayerHitten> playerHittens = new List<PlayerHitten>();

    [SerializeField]
    GameObject HitParticle;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float damage;
    [SerializeField]
    public float velocity;
    [SerializeField]
    MeleeWeaponTrail weaponTrail;
    [SerializeField]
    AudioClip HitSFX;

    [SerializeField]
    float volume;

    [SerializeField]
    public float AttackTime;

    bool IsAttacking = false;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(int ID)
    {
        IsAttacking = true;
        weaponTrail.Emit = true;
        playerHittens.Clear();
        PlayerID = ID;
        StartCoroutine(CancelAttack());
    }
    IEnumerator CancelAttack()
    {
        yield return new WaitForSeconds(AttackTime);
        IsAttacking = false;
        weaponTrail.Emit = false;
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsAttacking) return;
        if ((layerMask.value & 1 << other.gameObject.layer) > 0)
        {
            if (other.gameObject.transform.root.GetComponent<PlayerHitten>())
            {
                if (other.gameObject.transform.root.GetComponent<PlayerIdentity>().PlayerID == PlayerID) return;
                PlayerHitten hitten = other.gameObject.transform.root.GetComponent<PlayerHitten>();
                BulletHitInfo_AF bulletHitInfo = new BulletHitInfo_AF();
                bulletHitInfo.hitTransform = other.transform;
                bulletHitInfo.bulletForce = (other.ClosestPoint(transform.position) - transform.position).normalized * velocity;
                // bulletHitInfo.hitNormal = raycastHit.normal;
                bulletHitInfo.hitPoint = other.ClosestPoint(transform.position);

                if (!playerHittens.Contains(hitten))
                {
                    hitten.OnDamaged(damage, PlayerID);
                    hitten.OnHit(bulletHitInfo);
                    playerHittens.Add(hitten);
                    Instantiate(HitParticle, bulletHitInfo.hitPoint, Quaternion.identity);
                    audioSource.PlayOneShot(HitSFX, volume);
                }
            }
            else if (other.gameObject.transform.root.GetComponent<CreatureBasic>())
            {
                other.gameObject.transform.root.GetComponent<CreatureBasic>().Death();
                other.GetComponent<Rigidbody>().AddForceAtPosition((other.ClosestPoint(transform.position) - transform.position).normalized * velocity * 2, other.ClosestPoint(transform.position));
                Instantiate(HitParticle, other.ClosestPoint(transform.position), Quaternion.identity);
                audioSource.PlayOneShot(HitSFX, volume);
            }


            if (other.gameObject.tag == "Item")
            {
                if (other.gameObject.GetComponent<ItemBasic>())
                {
                    ItemBasic itemBasic = other.gameObject.GetComponent<ItemBasic>();

                    // 記得取消
                    // itemBasic.OnUse();

                    if (!itemBasic.IsHolded)
                    {
                        BulletHitInfo_AF bulletHitInfo = new BulletHitInfo_AF();
                        bulletHitInfo.hitTransform = other.transform;
                        bulletHitInfo.bulletForce = (other.ClosestPoint(transform.position) - transform.position).normalized * velocity / 30;
                        bulletHitInfo.hitPoint = other.ClosestPoint(transform.position);
                        itemBasic.AddForce(bulletHitInfo);
                    }
                }
            }
        }
    }
}
