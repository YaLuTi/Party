using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBombMother : ItemBasic
{
    [SerializeField]
    GameObject bomb;

    float cooldown = 0;
    float cooldownvalue = 20;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnTrigger()
    {
        for (int i = 0; i < UsingSound.Length; i++)
        {
            audioSource.PlayOneShot(UsingSound[i]);
        }
        Fire(0);
            /*
            if (basic_Bullets.Count > 1)
            {
                foreach (Basic_Bullet b in basic_Bullets)
                {
                    b.AddIgnore(basic_Bullets);
                }
            }*/

            if (Durability == 0)
            {
                Destroy(this.gameObject);
            }
        base.OnTrigger();
    }

    ItemBomb Fire(float angle)
    {
        GameObject b = Instantiate(bomb, FollowTransform.GetComponent<PlayerItemHand>().way.position + 1.2f * FollowTransform.GetComponent<PlayerItemHand>().way.transform.forward, FollowTransform.GetComponent<PlayerItemHand>().way.rotation);
        b.transform.eulerAngles += new Vector3(0, angle, 0);
        b.transform.localScale = transform.lossyScale; 
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(600 * (FollowTransform.GetComponent<PlayerItemHand>().way.forward)); // Need fix
        if (b.GetComponent<ItemBomb>() != null)
        {
            b.GetComponent<ItemBomb>().PlayerID = PlayerID;
            b.GetComponent<ItemBomb>().OnUse();
            return b.GetComponent<ItemBomb>();
        }
        return null;
    }


    public override void Enhance()
    {
        base.Enhance();

        transform.DOScale(transform.localScale * 2, 0.2f);
    }

    public override string OnUse(_playerItemStatus status)
    {
        Debug.Log("A");
        if (Durability <= 0) return "Empty";
        Durability--;
        if (status.Throwing) return "Empty";
        return base.OnUse(status);
    }
}
