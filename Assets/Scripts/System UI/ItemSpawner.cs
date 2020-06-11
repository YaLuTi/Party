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
    [SerializeField]
    SpawnEvent[] spawnEvents;
    int e = 0;

    public GameObject[] SpawnObject;

    List<GameObject> SpawnItem = new List<GameObject>();

    float CooldownCount;
    bool CooldownEnbale = true;
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
            CooldownEnbale = false;
            StartCoroutine(RunSpawnEvent());
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

        if(CooldownEnbale)
        CooldownCount += 60 * Time.deltaTime;
    }

    IEnumerator RunSpawnEvent()
    {
        float x = Random.Range(spawnEvents[e].xDistanceMin, spawnEvents[e].xDistanceMax);
        float z = Random.Range(spawnEvents[e].zDistanceMin, spawnEvents[e].zDistanceMax);


        for (int i = 0; i < spawnEvents[e].ItemQuantity; i++)
        {
            if (spawnEvents[e].IsAverage)
            {
                x = Mathf.Abs(x);
                z = Mathf.Abs(z);
                switch(i % 4)
                {
                    case 0:
                        x *= -1;
                        break;
                    case 1:
                        break;
                    case 2:
                        z *= -1;
                        break;
                    case 3:
                        x *= -1;
                        z *= -1;
                        break;
                }
            }
            else
            {
                x = Random.Range(spawnEvents[e].xDistanceMin, spawnEvents[e].xDistanceMax);
                z = Random.Range(spawnEvents[e].zDistanceMin, spawnEvents[e].zDistanceMax);
            }
            Vector3 p = spawnEvents[e].Offset + new Vector3(x, 0, z);
            int r = Random.Range(0, spawnEvents[e].Items.Length);
            SpawnItem.Add(Instantiate(spawnEvents[e].Items[r], p, Quaternion.identity));
            yield return new WaitForSeconds(spawnEvents[e].DropDelay);
        }
        CooldownEnbale = true;
        e++;
        if (e > spawnEvents.Length - 1) e = 0;
        yield return null;
    }
}

[System.Serializable]
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
