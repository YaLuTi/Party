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
        for(int i = 0; i < SpawnNum; i++)
        {
            yield return new WaitForSeconds(delay);
            GameObject b = Instantiate(bullet, transform.position + Distance * i * transform.forward  + Random.Range(-1, 1) * transform.right + new Vector3(0, 10, 0), Quaternion.Euler(90, 0, 0));
            
        }
    }
}
