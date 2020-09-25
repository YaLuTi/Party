using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemHand : MonoBehaviour
{
    [SerializeField]
    float ThrowStrength;

    [Header("Setting")]
    public Transform way;

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
        StartCoroutine(SetHoldItemDelay(item));
        //
    }

    IEnumerator SetHoldItemDelay(GameObject item)
    {
        HoldingItem = item;
        HoldingItem.GetComponent<Collider>().isTrigger = true;
        Rigidbody rb = HoldingItem.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        yield return new WaitForFixedUpdate();
        HoldingItem.transform.parent = transform;
        HoldingItem.transform.localPosition = Vector3.zero;
        HoldingItem.transform.localRotation = Quaternion.identity;
        HoldingItem.GetComponent<ItemBasic>().Hold();
        yield return null;
    }

    public void ThrowHoldingItem(float v)
    {
        HoldingItem.GetComponent<Collider>().isTrigger = false;
        HoldingItem.GetComponent<ItemBasic>().Throw();
        Rigidbody rb = HoldingItem.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(ThrowStrength * v * (way.forward)); // Need fix
        HoldingItem.transform.parent = null;
        HoldingItem = null;
    }

    public void DropHoldingItem(Vector3 velocity)
    {
        StartCoroutine(DropItem(velocity));
    }

    IEnumerator DropItem(Vector3 v)
    {
        HoldingItem.transform.parent = null;
        HoldingItem.GetComponent<ItemBasic>().IsHolded = false;
        yield return new WaitForFixedUpdate();
        HoldingItem.GetComponent<Collider>().isTrigger = false;
        Rigidbody rb = HoldingItem.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(v * 7f);

        HoldingItem = null;
        yield return null;
    }

    public void TriggerItem()
    {
        HoldingItem.GetComponent<ItemBasic>().OnTrigger();
    }

    public string UseItem(_playerItemStatus status)
    {
        return HoldingItem.GetComponent<ItemBasic>().OnUse(status);
    }
}
