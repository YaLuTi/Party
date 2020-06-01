using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ItemBomb : ItemBasic
{
    MeshRenderer meshRenderer;
    float Pow = 80;

    [Header("GameValue")]
    [SerializeField]
    public bool UseOnStart = false;
    [Header("FX")]
    [SerializeField]
    public GameObject ExplosionVFX;
    [SerializeField]
    ParticleSystem FuseParticle;
    [SerializeField]
    VisualEffect FuseVFX;
    [SerializeField]
    AudioSource FuseSFX;
    [SerializeField]
    float CameraShakePower = 0;

    public float delay;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (UseOnStart)
        {
            _playerItemStatus status = new _playerItemStatus();
            OnUse(status);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(Durability <= 0)
        {
            Pow -= Time.deltaTime * 45;
            Pow = Mathf.Max(Pow, 1);
            meshRenderer.material.SetFloat("Vector1_D1F6B343", Pow);
        }
    }

    public override string OnUse(_playerItemStatus status)
    {
        if (DurabilityCheck())
        {
            if (FuseParticle != null)
            {
                FuseParticle.Play();
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
        return base.OnUse(status);
    }
    
    IEnumerator Explosion()
    {
        float PauseTime = 0;
        yield return new WaitForSeconds(delay);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        CameraController.CameraShake(CameraShakePower);
        Destroy(this.gameObject);
        yield return 0;
    }
}
