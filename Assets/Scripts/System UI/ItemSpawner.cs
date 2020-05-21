using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;
    [SerializeField]
    float zOffset;
    [SerializeField]
    float xMinRange;
    [SerializeField]
    float xMaxRange;
    [SerializeField]
    float zMinRange;
    [SerializeField]
    float zMaxRange;

    [Header("SpawnEvent")]
    SpawnEvent[] spawnEvents;

    public GameObject[] SpawnObject;

    List<GameObject> SpawnItem = new List<GameObject>();

    float CooldownCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageController.IsPaused) return;
        if(CooldownCount > 140 + SpawnItem.Count * 10 && SpawnItem.Count < 25)
        {
            CooldownCount = 0;
            /*float x = Random.Range(xMinRange, xMaxRange);
            float z = Random.Range(zMinRange, zMaxRange);
            Vector3 p = new Vector3(xOffset + x, yOffset, zOffset + z);
            int i = Random.Range(0, SpawnObject.Length);
            SpawnItem.Add(Instantiate(SpawnObject[i], p, Quaternion.identity));*/
        }

        // 高耗能寫法  只用在測試時
        {
            List<int> delete = new List<int>();
            for (int i = 0; i < SpawnItem.Count; i++)
            {
                if (SpawnItem[i] == null)
                {
                    delete.Add(i);
                }
            }

            for (int i = 0; i < delete.Count; i++)
            {
                SpawnItem.RemoveAt(delete[i]);
            }
        }

        CooldownCount++;
    }

    IEnumerator RunSpawnEvent()
    {
        for (int i = 0; i < spawnEvents[0].ItemQuantity; i++)
        {
            float x = Random.Range(spawnEvents[0].xDistanceMin, spawnEvents[0].xDistanceMax);
            float z = Random.Range(spawnEvents[0].zDistanceMin, spawnEvents[0].zDistanceMax);
            Vector3 p = spawnEvents[0].Offset + new Vector3(x, 0, z);
            int r = Random.Range(0, spawnEvents[0].Items.Length);
            SpawnItem.Add(Instantiate(spawnEvents[0].Items[r], p, Quaternion.identity));
            yield return new WaitForSeconds(spawnEvents[0].DropDelay);
        }
        yield return null;
    }
}

public class SpawnEvent
{
    public int ItemQuantity;
    public GameObject[] Items;
    public float DropDelay;
    public bool IsAverage;
    public float xDistanceMin;
    public float xDistanceMax;
    public float zDistanceMin;
    public float zDistanceMax;
    public Vector3 Offset;
}
