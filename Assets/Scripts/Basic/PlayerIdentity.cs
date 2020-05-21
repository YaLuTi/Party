using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

public class PlayerIdentity : MonoBehaviour
{
    [SerializeField]
    Material[] MaterialsArray;
    [SerializeField]
    Material[] RingMaterialsArray;
    [SerializeField]
    DecalProjector RingDecal;
    [SerializeField]
    Vector3[] SpawnPosition;
    [SerializeField]
    Vector3[] SpawnRotation;

    public SkinnedMeshRenderer BodyMeshRenderer1;
    public SkinnedMeshRenderer BodyMeshRenderer2;

    Rigidbody[] rbs;

    PlayerInput playerInput;
    public int PlayerID;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
        PlayerID = playerInput.user.index;
        rbs = GetComponentsInChildren<Rigidbody>();

        Material[] mats = BodyMeshRenderer1.materials;
        mats[0] = MaterialsArray[PlayerID];
        BodyMeshRenderer1.materials = mats;
        mats = BodyMeshRenderer2.materials;
        mats[0] = MaterialsArray[PlayerID];
        BodyMeshRenderer2.materials = mats;

        RingDecal.material = RingMaterialsArray[PlayerID];

        StartCoroutine(SpawnToPosition());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnToPosition()
    {
        switch (PlayerID)
        {
            case 0:
                transform.position = SpawnPosition[0];
                transform.eulerAngles = SpawnRotation[0];
                break;
            case 1:
                transform.position = SpawnPosition[1];
                transform.eulerAngles = SpawnRotation[1];
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        yield return null;
    }
}
