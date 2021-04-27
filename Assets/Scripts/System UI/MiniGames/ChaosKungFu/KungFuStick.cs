using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KungFuStick : MonoBehaviour
{
    ObjectRotate objectRotate;

    // Start is called before the first frame update
    void Start()
    {
        objectRotate = GetComponentInParent<ObjectRotate>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.root.GetComponent<KungFuPlayerControll>().IsInvisable) return;
            if (collision.gameObject.transform.root.GetComponentInChildren<RagdollControl_AF>())
            {
                collision.gameObject.transform.root.GetComponentInChildren<RagdollControl_AF>().shotByBullet = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pause")
        {
            objectRotate.Way = !objectRotate.Way;
        }
    }
}
