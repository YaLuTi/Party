using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdentity : MonoBehaviour
{
    [SerializeField]
    Material[] MaterialsArray;
    [SerializeField]
    Vector3[] SpawnPosition;

    public SkinnedMeshRenderer r;

    PlayerInput playerInput;
    public int PlayerID;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        PlayerID = playerInput.user.index;

        Material[] mats = r.materials;
        mats[0] = MaterialsArray[PlayerID];
        r.materials = mats;

        switch (PlayerID)
        {
            case 0:
                transform.position = SpawnPosition[0];
                break;
            case 1:
                transform.position = SpawnPosition[1];
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
