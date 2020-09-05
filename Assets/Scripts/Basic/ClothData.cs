using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClothData : ScriptableObject
{

}

[CreateAssetMenu(menuName =("Create Cloth Array"))]
[System.Serializable]
public class ClothDataArray : ScriptableObject
{
    ClothData[] clothDatas;
}
