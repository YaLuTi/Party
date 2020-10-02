using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public List<Vector3> SpawnPosition;
    public List<Vector3> SpawnRotation;
    [SerializeField]
    float xMinRange;
    [SerializeField]
    float xMaxRange;
    [SerializeField]
    float zMinRange;
    [SerializeField]
    float zMaxRange;
}
