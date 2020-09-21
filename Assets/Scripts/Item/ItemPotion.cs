using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotion : ItemBasic
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
    AudioSource FuseSFX;

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
        if (Durability <= 0)
        {
            Pow -= Time.deltaTime * 43;
            Pow = Mathf.Max(Pow, 0);
            meshRenderer.material.SetFloat("_Noise_Power", Pow);
        }
    }

    public override string OnUse(_playerItemStatus status)
    {
        if (DurabilityCheck())
        {
            meshRenderer.material.SetFloat("_Noise_Power", Pow);
            meshRenderer.material.SetFloat("Vector1_C2A513C5", 1);


            StartCoroutine(Explosion());
        }
        else
        {
            return "Empty";
        }
        return base.OnUse(status);
    }

    public override void OnUse()
    {
        base.OnUse();
        if (DurabilityCheck())
        {
            /*if (FuseParticle != null)
            {
                FuseParticle.Play();
            }
            if (FuseVFX != null)
            {
                FuseVFX.Play();
            }*/
            meshRenderer.material.SetFloat("_Noise_Power", Pow);
            meshRenderer.material.SetFloat("Vector1_C2A513C5", 1);
            StartCoroutine(Explosion());
        }
    }

    public override void Throw()
    {
        base.Throw();
        OnUse();
    }

    IEnumerator Explosion()
    {
        float PauseTime = 0;
        yield return new WaitForSeconds(delay);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        // CameraController.CameraShake(CameraShakePower);
        Destroy(this.gameObject);
        yield return 0;
    }
}
