using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimFollow;

public class Mine_Explosion : ItemMine
{
    [SerializeField]
    public GameObject ExplosionVFX;
    [SerializeField]
    float ExplosionDelay = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void SettedEvent()
    {
        base.SettedEvent();
        Collider[] rays = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < rays.Length; i++)
        {
            if (rays[i].transform.root.gameObject.tag == "Player")
            {
                if (rays[i].gameObject.transform.root.GetComponent<PlayerHitten>())
                {
                    StartCoroutine(Explosion());
                    IsSetted = false;
                    break;
                }
            }
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSecondsRealtime(ExplosionDelay);
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return 0;
    }
}
