using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KungFuManager : MonoBehaviour
{
    public GameObject KungFuPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StageManager.instance.OnPlayerJoin += PlayerJoin;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlayerJoin(GameObject player, int num)
    {
        Vector3 p = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnPosition[num];
        Vector3 r = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfo>().SpawnRotation[num];
        GameObject g = Instantiate(KungFuPlayer, p, Quaternion.Euler(r));
        g.GetComponent<KungFuPlayerControll>().Set(player);
    }
}
