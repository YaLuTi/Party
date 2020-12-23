using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AxeSpawner : MonoBehaviour
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

    public GameObject Crack;

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
                GameObject g = Instantiate(spawnEvents[i].Item, spawnEvents[i].positoin[r] + new Vector3(0, 10, 0), Quaternion.Euler(spawnEvents[i].rotation[r] + new Vector3(90, 0, 0)));
                g.transform.DOMoveY(spawnEvents[i].positoin[r].y, 0.5f).SetEase(Ease.InCubic);
                g.transform.DORotate(spawnEvents[i].rotation[r], 0.5f).SetEase(Ease.InCubic);
                StartCoroutine(CameraShake(r));
                SpawnItem.Add(g);
                /*CinemachineTargetGroup targetGroup = GameObject.FindGameObjectWithTag("CineGroup").GetComponent<CinemachineTargetGroup>();
                targetGroup.AddMember(g.transform, 0.2f, 0);*/
                return;
            }
        }
    }

    IEnumerator CameraShake(int r)
    {
        yield return new WaitForSeconds(0.5f);
        CameraController.CameraShake(8);
        GameObject g = Instantiate(Crack, spawnEvents[0].positoin[r] + new Vector3(-0.5f, -0.9f, 0),  Quaternion.identity);
        yield return new WaitForSeconds(6f);
        // g.transform.DOScaleY(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(g);
        yield return null;
    }
}
