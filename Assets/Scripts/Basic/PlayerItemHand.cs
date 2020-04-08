using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHand : MonoBehaviour
{
    [SerializeField]
    float ThrowStrength;


    public GameObject HoldingItem = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHoldingItem(GameObject item)
    {
        HoldingItem = item;
        HoldingItem.transform.parent = transform;
        HoldingItem.transform.localPosition = Vector3.zero;
        HoldingItem.transform.localRotation = Quaternion.identity;
        //
        HoldingItem.GetComponent<Collider>().isTrigger = true;
        Rigidbody rb = HoldingItem.GetComponent<Rigidbody>();
        // rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

    public void ThrowHoldingItem()
    {
        HoldingItem.GetComponent<Collider>().isTrigger = false;
        Rigidbody rb = HoldingItem.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(ThrowStrength * transform.right);
        HoldingItem.transform.parent = null;
        HoldingItem = null;
    }
}
