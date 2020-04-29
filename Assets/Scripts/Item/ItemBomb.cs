using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : ItemBasic
{
    [SerializeField]
    public GameObject ExplosionVFX;
    [SerializeField]
    ParticleSystem FuseParticle;
    [SerializeField]
    AudioSource FuseSFX;
    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string OnUse()
    {
        if (DurabilityCheck())
        {
            FuseParticle.Play();
            FuseSFX.Play();
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
