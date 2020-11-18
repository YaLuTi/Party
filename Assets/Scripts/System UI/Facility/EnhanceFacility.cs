using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnhanceFacility : FacilityArea
{
    public ItemBasic item;
    public Vector3 p;
    float Color_Intensity = 1f;
    Color c;

    [SerializeField]
    float EnhanceTime;

    bool IsUsed = false;

    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        float factor = Mathf.Pow(2, Color_Intensity);
        c = material.GetColor("_EmissionColor");
        Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
        material.SetColor("_EmissionColor", color);
    }

    // Update is called once per frame
    void Update()
    {
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

        yield return new WaitForSeconds(0.4f);

        g.transform.DOMoveY(g.transform.position.y + 1, EnhanceTime).SetEase(Ease.OutSine);
        while(Color_Intensity < 2.5f)
        {
            float factor = Mathf.Pow(2, Color_Intensity);
            Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
            material.SetColor("_EmissionColor", color);
            Color_Intensity += (2.5f / EnhanceTime) * Time.deltaTime;
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
        
        while (Color_Intensity > 0f)
        {
            float factor = Mathf.Pow(2, Color_Intensity);
            Color color = new Color(c.r * factor, c.g * factor, c.b * factor);
            material.SetColor("_EmissionColor", color);
            Color_Intensity -= (2.5f / EnhanceTime) * 2 * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        transform.DOScale(Vector3.zero, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);

        yield return null;
    }
}
