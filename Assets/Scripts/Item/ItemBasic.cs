using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBasic : MonoBehaviour
{
    Rigidbody rb;
    bool IsThrowing = false;
    int PlayerID = -1;

    [Header("GameValue")]
    [SerializeField]
    public float Durability = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void OnUse()
    {
        if(Durability > 0)
        {
            Durability--;
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsThrowing) return;
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerPickItem>().OnHit(collision.contacts[0].point, rb.velocity);
        }
        Debug.Log(rb.velocity);
    }
}
