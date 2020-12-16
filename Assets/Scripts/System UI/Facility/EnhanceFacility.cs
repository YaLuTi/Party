using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnhanceFacility : FacilityArea
{
    public ItemBasic item;
    public Vector3 p;
    float Color_Intensity = 0f;
    Color c;

    [SerializeField]
    float EnhanceTime;

    bool IsUsed = false;

    Material material;

    public AudioClip[] audioClips;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        float factor = Mathf.Pow(1.6f, Color_Intensity);
        c = material.GetColor("_EmissionColor");
        Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
        material.SetColor("_EmissionColor", color);

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(_SpawnAnimation());
    }

    // Update is called once per frame
    void Update()
    {/*
        if (!IsUsed)
        {
            Color_Intensity = Mathf.PingPong(Time.time / 5, 1.2f);
            Debug.Log(Color_Intensity);
            float factor = Mathf.Pow(0.6f, Color_Intensity);
            Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
            material.SetColor("_EmissionColor", color);
        }*/
    }

    public override void OnUse(PlayerBehavior playerBehavior)
    {
        if (IsUsed) return;
        base.OnUse(playerBehavior);
        if (playerBehavior.IsHolding)
        {
            GameObject g = playerBehavior.itemHand.HoldingItem;
            if (g.GetComponent<ItemBasic>().Durability <= 0) return;
            playerBehavior.OnHit();
            
            g.GetComponent<Collider>().isTrigger = true;
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            g.GetComponent<ItemBasic>().Put(this.transform);

            IsUsed = true;
            StartCoroutine(_PutAnimation(g));
        }
    }

    IEnumerator _PutAnimation(GameObject g)
    {
        // g.transform.DOMoveY(g.transform.position.y + 0.5f, 0.6f).SetEase(Ease.OutQuint);
        g.transform.DOLocalMove(p, 2f).SetEase(Ease.OutQuint);
        audioSource.PlayOneShot(audioClips[0]);

        yield return new WaitForSeconds(0.4f);

        g.transform.DOMoveY(g.transform.position.y + 1, EnhanceTime).SetEase(Ease.OutSine);
        while(Color_Intensity < 1.6f)
        {
            float factor = Mathf.Pow(1.6f, Color_Intensity);
            Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
            material.SetColor("_EmissionColor", color);
            Color_Intensity += (1.6f / EnhanceTime) * Time.deltaTime;
            yield return null;
        }

        g.GetComponent<Collider>().isTrigger = false;
        Rigidbody rb = g.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
        g.transform.parent = null;

        if (g.GetComponentInChildren<PSMeshRendererUpdater>(true))
        {
            g.GetComponentInChildren<PSMeshRendererUpdater>(true).transform.gameObject.SetActive(true);
            g.GetComponentInChildren<PSMeshRendererUpdater>().UpdateMeshEffect();
        }

        g.GetComponent<ItemBasic>().IsHolded = false;
        g.GetComponent<ItemBasic>().Enhance();
        audioSource.PlayOneShot(audioClips[1]);

        while (Color_Intensity > 0f)
        {
            float factor = Mathf.Pow(1.6f, Color_Intensity);
            Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
            material.SetColor("_EmissionColor", color);
            Color_Intensity -= (1.6f / EnhanceTime) * 3.5f * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);

        yield return null;
    }

    IEnumerator _SpawnAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.position += new Vector3(0, 2, 0);
        transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutQuart);
        // transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.WorldAxisAdd).SetEase(Ease.InSine);
        yield return new WaitForSeconds(0.4f);
        transform.DOMoveY(transform.position.y - 2, 0.6f).SetEase(Ease.OutQuart);
        yield return null;
    }
}
