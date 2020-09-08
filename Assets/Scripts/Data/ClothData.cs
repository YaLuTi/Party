using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Create Cloth Data"))]
[System.Serializable]
public class ClothData : ScriptableObject
{
    public string name;
    public int type;// 0 hat 1 face
    public GameObject cloth;
    public Vector3 PositionOffset;
    public Vector3 RotationOffset;
    public Vector3 ScaleOffset;
}
