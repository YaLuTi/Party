using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUltimateBombChicken : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public GameObject Explosion;
    float f;
    bool b = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Event());
    }

    // Update is called once per frame
    void Update()
    {
        if (b)
        {
            f += Time.deltaTime;
            Material[] mats = meshRenderer.materials;
            for(int i = 0; i < mats.Length; i++)
            {
                mats[i].SetFloat("_Transparent_Multiplier", f);
            }
        }
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(2.3f);
        b = true;
        yield return new WaitForSeconds(1);
        GameObject g = Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(this.transform.parent.parent.gameObject);
        yield return null;
    }
}
