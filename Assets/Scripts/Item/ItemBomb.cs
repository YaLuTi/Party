using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ItemBomb : ItemBasic
{
    MeshRenderer meshRenderer;
    float Pow = 80;

    [SerializeField]
    public bool UseOnStart = false;
    [SerializeField]
    public GameObject ExplosionVFX;
    [SerializeField]
    ParticleSystem FuseParticle;
    [SerializeField]
    VisualEffect FuseVFX;
    [SerializeField]
    AudioSource FuseSFX;

    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (UseOnStart)
        {
            OnUse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Durability <= 0)
        {
            Pow -= Time.deltaTime * 45;
            Pow = Mathf.Max(Pow, 1);
            meshRenderer.material.SetFloat("Vector1_D1F6B343", Pow);
        }
    }

    public override string OnUse()
    {
        if (DurabilityCheck())
        {
            if (FuseParticle != null)
            {
                FuseParticle.Play();
            }
            if (FuseSFX != null)
            {
                FuseSFX.Play();
            }
            if(FuseVFX != null)
            {
                FuseVFX.Play();
            }
            meshRenderer.material.SetFloat("Vector1_D1F6B343", Pow);
            meshRenderer.material.SetFloat("Vector1_C2A513C5", 1);


            StartCoroutine(Explosion());
        }
        else
        {
            return "Empty";
        }
        return base.OnUse();
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSecondsRealtime(delay);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return 0;
    }
}
