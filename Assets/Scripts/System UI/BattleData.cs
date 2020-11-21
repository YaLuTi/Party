using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleData : MonoBehaviour
{
    public static float[] Players_DealDamage;
    // Start is called before the first frame update
    void Start()
    {
        Players_DealDamage = new float[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddDamage(int ID, float damage)
    {
        if (ID >= 0)
        {
            Players_DealDamage[ID] += damage;
        }
    }

    public static void TEST_END_SHOW()
    {
        for(int i = 0; i < Players_DealDamage.Length; i++)
        {
            Debug.Log(Players_DealDamage[i]);
        }
    }
}
