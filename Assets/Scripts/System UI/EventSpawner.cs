using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EventSpawner : MonoBehaviour
{

    [Header("SpawnEvent")]
    public List<SpawnEvent> spawnEvents = new List<SpawnEvent>();
    int e = 0;

    List<GameObject> SpawnItem = new List<GameObject>();

    [HideInInspector]
    public float CooldownCount;
    public float CooldownValue = 30;
    public float CooldownValuePerItem = 20;
    bool CooldownEnbale = true;

    RoamingAI roamingAI;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (StageController.IsPaused || !StageManager.InGame) return;
        if (CooldownCount > CooldownValue + SpawnItem.Count * CooldownValuePerItem && SpawnItem.Count < 1)
        {
            CooldownCount = 0;
            RunSpawnItem();
        }

        // 高耗能寫法  只用在測試時
        {
            List<int> delete = new List<int>();
            for (int i = 0; i < SpawnItem.Count; i++)
            {
                if (SpawnItem[i] == null)
                {
                    delete.Add(i);
                    CooldownCount /= 5;
                }
            }

            for (int i = 0; i < delete.Count; i++)
            {
                SpawnItem.RemoveAt(delete[i]);
            }
        }

        CooldownCount += 60 * Time.deltaTime;
    }

    void RunSpawnItem()
    {
        float range = 0;
        for (int i = 0; i < spawnEvents.Count; i++)
            range += spawnEvents[i].Weights;

        float rand = Random.Range(0, range);
        float top = 0;

        for (int i = 0; i < spawnEvents.Count; i++)
        {
            top += spawnEvents[i].Weights;
            if (rand < top)
            {
                int r = Random.Range(0, spawnEvents[i].positoin.Length);
                Debug.Log(r);
                GameObject g = Instantiate(spawnEvents[i].Item, spawnEvents[i].positoin[r], Quaternion.Euler(spawnEvents[i].rotation[r]));
                SpawnItem.Add(g);
                CinemachineTargetGroup targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
                targetGroup.AddMember(g.transform, 0.2f, 0);
                return;
            }
        }
    }
}
[System.Serializable]
public class SpawnEvent
{
    public float Weights;
    public GameObject Item;
    public Vector3[] positoin;
    public Vector3[] rotation;
}
