using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimFollow;

[RequireComponent(typeof(AudioSource))]
public class ItemBasic : MonoBehaviour
{
    Rigidbody rb;
    bool IsThrowing = false;
    public bool IsHolded = false;
    int PlayerID = -1;

    public GameObject HighLight;
    public int PlayerSelecting = 0;

    [Header("SFX")]
    protected AudioSource audioSource;
    public AudioClip[] UsingSound;

    [Header("GameValue")]
    [SerializeField]
    public float Durability = 1;
    [SerializeField]
    Vector3 HoldedPosition;
    [SerializeField]
    Vector3 HoldedRotation;

    public string animation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (HighLight != null)
        {
            if (PlayerSelecting > 0)
            {
                HighLight.SetActive(true);
            }
            else
            {
                HighLight.SetActive(false);
            }
        }
    }

    public virtual void Throw()
    {
        StartCoroutine(ThrowEvent());
    }

    IEnumerator ThrowEvent()
    {
        yield return new WaitForSeconds(0.5f);
        IsHolded = false;
        yield return null;
    }

    public void PickHighlight()
    {
        PlayerSelecting++;
    }

    public void CancelHighlight()
    {
        PlayerSelecting--;
    }

    public virtual string OnUse(_playerItemStatus status)
    {
        return animation;
    }

    public virtual void OnUse()
    {
    }

    public virtual void OnTrigger()
    {

    }

    public void Hold()
    {
        transform.localPosition = HoldedPosition;
        transform.localRotation = Quaternion.Euler(HoldedRotation);
        IsHolded = true;
    }

    public bool DurabilityCheck()
    {
        if (Durability > 0)
        {
            Durability--;
            for (int i = 0; i < UsingSound.Length; i++)
            {
                audioSource.PlayOneShot(UsingSound[i]);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddForce(BulletHitInfo_AF bulletHitInfo)
    {
        bulletHitInfo.hitTransform.GetComponent<Rigidbody>().AddForceAtPosition(bulletHitInfo.bulletForce, bulletHitInfo.hitPoint);
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
