using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIceBook : Item_Staff
{
    public float charge = 1;
    public float MaxCharge = 5;
    public float ChargeSpeed = 3;
    bool IsCharging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Update()
    {
        base.Update();
        if (IsCharging && charge < MaxCharge) charge += Time.deltaTime * ChargeSpeed;
    }

    public override void OnTrigger()
    {
        if (DurabilityCheck())
        {
            StartCoroutine(Fire());
            if (Durability == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Fire()
    {
        float num = Mathf.Floor(charge);
        num = Mathf.Min(MaxCharge, charge);
        charge = 1;

        Vector3 p = FollowTransform.GetComponent<PlayerItemHand>().way.position;
        Vector3 forward = transform.forward;
        Quaternion quaternion = FollowTransform.GetComponent<PlayerItemHand>().way.rotation;

        for (int i = 0; i < num; i++)
        {
            GameObject b = Instantiate(bullet, p + new Vector3(0, 1, 0) + -1.5f * forward, quaternion);
            // b.transform.eulerAngles += new Vector3(0, Random.Range(4, -4), 0);
            Destroy(b, DestroyTime);
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    public override string OnUse(_playerItemStatus status)
    {
        IsCharging = true;
        return base.OnUse(status);
    }

    public override string OnRelease(_playerItemStatus status)
    {
        IsCharging = false;
        if (charge == 1) return "";
        return base.OnRelease(status);
    }

    public override void Throw()
    {
        base.Throw();
        IsCharging = false;
        charge = 1;
    }
}
