using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEnhanceSpawn : MonoBehaviour
{
    [SerializeField]
    int SpawnNum;
    [SerializeField]
    float Distance;
    [SerializeField]
    float delay;
    [SerializeField]
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator _Spawn()
    {
        for(int i = 1; i < SpawnNum; i++)
        {
            for (int j = 0; j < (i / 2) + 2; j++)
            {
                yield return new WaitForSeconds(delay);
                GameObject b = Instantiate(bullet, transform.position + ((Distance * i) + 1.2f) * transform.forward + ((i * -1f + (j * 2f)) * transform.right) + new Vector3(Random.Range(-0.5f,0.5f), 10, Random.Range(-0.5f, 0.5f)), Quaternion.Euler(90, 0, 0));
            }
        }
    }
}
