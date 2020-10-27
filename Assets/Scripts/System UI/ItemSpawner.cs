using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemSpawner : MonoBehaviour
{

    [Header("SpawnEvent")]
    [HideInInspector]
    public List<SpawnEvent> spawnEvents = new List<SpawnEvent>();
    int e = 0;

    [HideInInspector]
    public GameObject[] SpawnObject;

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
        roamingAI = GetComponent<RoamingAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StageController.IsPaused) return;
        if(CooldownCount > CooldownValue + SpawnItem.Count * CooldownValuePerItem/* && SpawnItem.Count < 25*/)
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
                Vector3 p = roamingAI.PickNewDestination();
                p.y = 10;
                SpawnItem.Add(Instantiate(spawnEvents[i].Item, p, spawnEvents[i].Item.transform.rotation));
                return;
            }
        }
    }

    /*IEnumerator RunSpawnEvent()
    {
        for (int i = 0; i < spawnEvents[e].ItemQuantity; i++)
        {
            Vector3 p = Vector3.zero;
            int r = Random.Range(0, spawnEvents[e].Items.Length);
            SpawnItem.Add(Instantiate(spawnEvents[e].Items[r], p, Quaternion.identity));
            yield return new WaitForSeconds(spawnEvents[e].DropDelay);
        }
        CooldownEnbale = true;
        e++;
        if (e > spawnEvents.Count - 1) e = 0;
        yield return null;
    }*/
}

[System.Serializable]
public class SpawnEvent
{
    public float Weights;
    public GameObject Item;
}
/*
[CustomEditor(typeof(ItemSpawner))]
[CanEditMultipleObjects]
public class ItemSpawnerEditor : Editor
{
    ItemSpawner m_Target;

    public override void OnInspectorGUI()
    {
        m_Target = (ItemSpawner)target;
        DrawDefaultInspector();
        DrawTypesInspector();
    }
    void DrawTypesInspector()
    {
        GUILayout.Space(5);
        GUILayout.Label("Item List", EditorStyles.boldLabel);

        for (int i = 0; i < m_Target.spawnEvents.Count; i++)
        {
            GUILayout.Space(3);
            DrawType(i);
        }

        DrawAddTypeButton();
    }

    void DrawType(int index)
    {
        if (index < 0 || index >= m_Target.spawnEvents.Count)
            return;

        GUILayout.BeginHorizontal();
        {

            // BeginChangeCheck() 用來檢查在 BeginChangeCheck() 和 EndChangeCheck() 之間是否有 Inspector 變數改變
            EditorGUI.BeginChangeCheck();
            EditorGUIUtility.labelWidth = 30.0f;
            GameObject newGameobject = (GameObject)EditorGUILayout.ObjectField("Item", m_Target.spawnEvents[index].Item, typeof(GameObject), true, GUILayout.Width(260));
            GUILayout.Label("Weights", EditorStyles.label, GUILayout.Width(50));
            float newWeights = EditorGUILayout.FloatField(m_Target.spawnEvents[index].Weights, GUILayout.Width(50));

            m_Target.spawnEvents[index].Weights = newWeights;
            m_Target.spawnEvents[index].Item = newGameobject;

            // 如果 Inspector 變數有改變，EndChangeCheck() 會回傳 True，才有必要去做變數存取
            if (EditorGUI.EndChangeCheck())
            {
                // 在修改之前建立 Undo/Redo 記錄步驟
                Undo.RecordObject(m_Target, "Modify Types");

                m_Target.spawnEvents[index].Weights = newWeights;
                m_Target.spawnEvents[index].Item = newGameobject;

                // 每當直接修改 Inspector 變數，而不是使用 serializedObject 修改時，必須要告訴 Unity 這個 Compoent 已經修改過了
                // 在下一次存檔時，必須要儲存這個變數
                EditorUtility.SetDirty(m_Target);
            }

            if (GUILayout.Button("Remove"))
            {
                // 系統會 "登" 一聲
                EditorApplication.Beep();

                // 顯示對話框功能(帶有 OK 和 Cancel 兩個按鈕)
                if (EditorUtility.DisplayDialog("Really?", "Do you really want to remove the state '" + m_Target.spawnEvents[index].Item.name + "'?", "Yes", "No") == true)
                {
                    m_Target.spawnEvents.RemoveAt(index);
                    EditorUtility.SetDirty(m_Target);
                }

            }
        }
        GUILayout.EndHorizontal();
    }

    void DrawAddTypeButton()
    {
        if (GUILayout.Button("Add new Item", GUILayout.Height(30)))
        {
            Undo.RecordObject(m_Target, "Add new Type");

            m_Target.spawnEvents.Add(new SpawnEvent { Weights = 0 });
            EditorUtility.SetDirty(m_Target);
        }
    }
}*/