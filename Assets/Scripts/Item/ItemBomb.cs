using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : ItemBasic
{
    [SerializeField]
    public GameObject ExplosionVFX;
    [SerializeField]
    float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnUse()
    {
        base.OnUse();
        StartCoroutine(Explotion());
    }

    IEnumerator Explotion()
    {
        yield return new WaitForSecondsRealtime(delay);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return 0;
    }
}
