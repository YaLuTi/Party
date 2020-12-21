using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item_Melee : ItemBasic
{
    public MeleeWeaponCollider weaponCollider;

    public override void OnTrigger()
    {
        base.OnTrigger();
        weaponCollider.Attack(PlayerID);
        audioSource.PlayOneShot(UsingSound[0]);
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
    }

    public override void Throw()
    {
        base.Throw();
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
