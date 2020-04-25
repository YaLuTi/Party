using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHitten : MonoBehaviour
{
    RagdollControl_AF ragdollControl;
    PlayerPickItem pickItem;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        ragdollControl = GetComponentInChildren<RagdollControl_AF>();
        pickItem = GetComponentInChildren<PlayerPickItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnHit(BulletHitInfo_AF info)
    {
        ragdollControl.shotByBullet = true;
        pickItem.OnHit(info);
        StartCoroutine(AddForceToLimb(info));
    }

    IEnumerator AddForceToLimb(BulletHitInfo_AF bulletHitInfo)
    {
        yield return new WaitForFixedUpdate();
        bulletHitInfo.hitTransform.GetComponent<Rigidbody>().AddForceAtPosition(bulletHitInfo.bulletForce, bulletHitInfo.hitPoint);

    }
}
