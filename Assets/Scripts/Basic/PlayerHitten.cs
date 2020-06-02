using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHitten : MonoBehaviour
{
    [SerializeField]
    RagdollControl_AF ragdollControl;
    PlayerBehavior pickItem;
    public bool test;
    public Transform Hips;
    public Transform foot;
    public Transform FaceWay;
    public GameObject Decal;

    public FlagScore Flag;
    // Start is called before the first frame update
    void Start()
    {
        pickItem = GetComponentInChildren<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Decal.transform.position = Hips.transform.position;
        Vector3 v = Decal.transform.position;
        v.y = foot.position.y;
        // Decal.transform.position = v;
    }
    public void OnHit(BulletHitInfo_AF info)
    {
        ragdollControl.shotByBullet = true;
        if (Flag != null) Flag.Throw();
        Flag = null;
        pickItem.OnHit(info);
        StartCoroutine(AddForceToLimb(info));
    }

    public bool IsGettingUp()
    {
        return ragdollControl.PlayerInhibit();
    }

    IEnumerator AddForceToLimb(BulletHitInfo_AF bulletHitInfo)
    {
        yield return new WaitForFixedUpdate();
        if (bulletHitInfo.hitPoint != null)
        {
            bulletHitInfo.hitTransform.GetComponent<Rigidbody>().AddForceAtPosition(bulletHitInfo.bulletForce, bulletHitInfo.hitPoint);
        }

    }
}
