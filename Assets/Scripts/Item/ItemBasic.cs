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
    public Transform FollowTransform;
    public int PlayerID = -1;

    public GameObject HighLight;
    public int PlayerSelecting = 0;

    [Header("SFX")]
    protected AudioSource audioSource;
    public AudioClip[] UsingSound;

    [Header("GameValue")]
    [SerializeField]
    public int Durability = 1;
    public int MaxDurability = 1;
    [SerializeField]
    protected Vector3 HoldedPosition;
    [SerializeField]
    protected Vector3 HoldedRotation;

    public string animation;
    public string Releaseanimation;

    public string IdleAnimation;

    [SerializeField]
    protected bool Enhaced = false;

    [SerializeField]
    protected GameObject UI_Icon;
    protected GameObject UI_IconCopy;

    public delegate void ItemTriggerHandler(int v);
    public event ItemTriggerHandler TriggerEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        MaxDurability = Durability;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        /*
        if (UI_Icon != null)
        {
            UI_IconCopy = Instantiate(UI_Icon);
            UI_IconCopy.GetComponent<UIFollow>().follow = this.transform;
            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            if (canvas != null)
            {
                UI_IconCopy.transform.parent = canvas.transform;
                UI_IconCopy.transform.localScale = new Vector3(1, 1, 1);
            }
        }*/
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

        if(transform.position.y <= -30)
        {
            Destroy(this.gameObject);
        }

        /*if (IsHolded)
        {
            transform.position = FollowTransform.position + HoldedPosition;
            transform.rotation = Quaternion.Euler(HoldedRotation) * FollowTransform.rotation;
        }*/
    }

    public virtual void Throw()
    {
        StartCoroutine(ThrowEvent());
        PlayerID = -1;
    }

    protected IEnumerator ThrowEvent()
    {
        transform.parent = null;
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

    // 由ItemHand呼叫
    public virtual string OnUse(_playerItemStatus status)
    {
        return animation;
    }

    public virtual void OnUse()
    {
    }

    public virtual void Enhance()
    {
        Enhaced = true;
    }

    public virtual string OnRelease(_playerItemStatus status)
    {
        return Releaseanimation;
    }

    public virtual void OnRelease()
    {

    }

    public virtual void OnTrigger()
    {
        TriggerEvent?.Invoke(Durability);
        if (Durability <= 0 && IdleAnimation != null)
        {
            transform.root.GetComponentInChildren<Animator>().SetBool(IdleAnimation, false);
        }
    }
    
    public virtual void OnTriggerEnd()
    {

    }

    public virtual void Hold(Transform t)
    {
        transform.parent = t;
        transform.localPosition = HoldedPosition;
        transform.localRotation = Quaternion.Euler(HoldedRotation);
        IsHolded = true;
        if (GetComponentInParent<PlayerIdentity>())
        {
            PlayerID = GetComponentInParent<PlayerIdentity>().PlayerID;
        }
    }

    // 這邊寫的挺爛的 應該要由Call的人去Set位置

    public void Put(Transform t)
    {
        transform.parent = t;
        IsHolded = true;
        if (GetComponentInParent<PlayerIdentity>())
        {
            PlayerID = GetComponentInParent<PlayerIdentity>().PlayerID;
        }
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
