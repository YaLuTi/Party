using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScore : MonoBehaviour
{
    [SerializeField]
    Vector3 spawnPosition;
    [SerializeField]
    LayerMask layerMask;

    public static int id = -1;
    public static Transform follow;

    Rigidbody rb;
    bool IsHolded = false;
    int cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            transform.position = follow.position + new Vector3(0, 2, 0);
            if (cooldown > 45)
            {
                cooldown = 0;
                ScoreManager.AddScore(id, 1);
            }
            cooldown++;
        }
        else
        {
            cooldown = 0;
        }


        if (!IsHolded)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1, layerMask);
            foreach (Collider collider in colliders)
            {
                if ((layerMask.value & 1 << collider.gameObject.layer) > 0)
                {
                    if (collider.transform.root.GetComponent<PlayerHitten>().IsGettingUp()) continue;
                    IsHolded = true;
                    transform.parent = collider.transform.root;
                    id = GetComponentInParent<PlayerIdentity>().PlayerID;
                    follow = GetComponentInParent<PlayerHitten>().Hips;
                    GetComponentInParent<PlayerHitten>().Flag = this;
                    rb.isKinematic = true;
                    break;
                }
            }
        }

        if (transform.position.y < -5 || transform.position.y > 15)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.position = spawnPosition;
        }
    }

    public void Throw()
    {
        IsHolded = false;
        transform.parent = null;
        id = -1;
        follow = null;
        rb.isKinematic = false;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (IsHolded) return;
        if (other.transform.root.tag == "Player")
        {
            Debug.Log("B");
            IsHolded = true;
            transform.parent = other.transform.root;
            id = GetComponentInParent<PlayerIdentity>().PlayerID;
            follow = GetComponentInParent<PlayerHitten>().Hips;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (IsHolded) return;
        if (collision.transform.root.tag == "Player")
        {
            IsHolded = true;
            transform.parent = collision.transform.root;
            id = GetComponentInParent<PlayerIdentity>().PlayerID;
            follow = GetComponentInParent<PlayerHitten>().Hips;
            GetComponentInParent<PlayerHitten>().Flag = this;
            rb.isKinematic = true;
        }
    }
}
