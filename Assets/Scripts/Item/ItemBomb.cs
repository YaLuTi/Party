using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : ItemBasic
{
    [SerializeField]
    public GameObject ExplotionVFX;
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
        yield return new WaitForSecondsRealtime(2);
        Instantiate(ExplotionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return 0;
    }
}
