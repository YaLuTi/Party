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
        }
    }

    public override string OnUse(_playerItemStatus status)
    {
        if (DurabilityCheck())
        {


            StartCoroutine(Explosion());
        }
        else
        {
            return "Empty";
        }
        return base.OnUse(status);
    }

    public override void Throw()
    {
        base.Throw();
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
