using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item_Melee : ItemBasic
{
    public MeleeWeaponCollider weaponCollider;
    Transform p;
    bool MeleeHold;

    public override void OnTrigger()
    {
        base.OnTrigger();
        weaponCollider.Attack(PlayerID);
        audioSource.PlayOneShot(UsingSound[0]);
    }

    public override void Update()
    {
        base.Update();
        if(MeleeHold && !IsHolded)
        {
            p.root.GetComponentInChildren<Animator>().SetBool(IdleAnimation, false);
            MeleeHold = false;
        }
    }

    public override void OnTriggerEnd()
    {
        base.OnTriggerEnd();
        Durability--;
        if(Durability <= 0)
        {
            transform.root.GetComponentInChildren<Animator>().SetBool(IdleAnimation, false);
            Destroy(this.gameObject);
        }
    }

    public override void Hold(Transform t)
    {
        base.Hold(t);
        p = t;
        MeleeHold = true;
    }

    public override void Throw()
    {
        p.root.GetComponentInChildren<Animator>().SetBool(IdleAnimation, false);
        base.Throw();
    }

    private void OnDestroy()
    {
        p.root.GetComponentInChildren<Animator>().SetBool(IdleAnimation, false);
    }

    public override void Enhance()
    {
        base.Enhance();
        Vector3 s = transform.localScale;
        s += new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(s, 0.1f);
        weaponCollider.velocity += 30000;
    }
}
