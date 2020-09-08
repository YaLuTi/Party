using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Create Cloth Array"))]
[System.Serializable]
public class ClothDataArray : ScriptableObject
{
    public ClothData[] clothDatas;
}

