using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KungFuStick : MonoBehaviour
{
    ObjectRotate objectRotate;
    AudioSource audioSource;
    [SerializeField]
    AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        objectRotate = GetComponentInParent<ObjectRotate>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectRotate.End)
        {
            this.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.root.GetComponent<KungFuPlayerControll>().IsInvisable) return;
            if (collision.gameObject.transform.root.GetComponentInChildren<RagdollControl_AF>())
            {
                collision.gameObject.transform.root.GetComponentInChildren<RagdollControl_AF>().shotByBullet = true;
                collision.gameObject.transform.root.GetComponent<KungFuPlayerControll>().Death();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pause")
        {
            objectRotate.Way = !objectRotate.Way;
            audioSource.PlayOneShot(audioClip);
            objectRotate.speed++;
        }
    }
}
