using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using AnimFollow;

public class PlayerIdentity : MonoBehaviour
{
    [SerializeField]
    Material[] MaterialsArray;
    [SerializeField]
    Material[] RingMaterialsArray;
    [SerializeField]
    DecalProjector RingDecal;
    [SerializeField]
    Transform playerMove;
    [SerializeField]
    Transform playerRig;
    [SerializeField]
    SimpleFootIK_AF footIK_AF;
    StageInfo stageInfo;
    [SerializeField]
    Vector3[] SpawnPosition;
    [SerializeField]
    Vector3[] SpawnRotation;

    public SkinnedMeshRenderer BodyMeshRenderer1;
    public SkinnedMeshRenderer BodyMeshRenderer2;

    Rigidbody[] rbs;
    Collider[] colliders;

    PlayerInput playerInput;
    public int PlayerID;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
        PlayerID = playerInput.user.index;
        rbs = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();

        Material[] mats = BodyMeshRenderer1.materials;
        mats[0] = MaterialsArray[PlayerID];
        BodyMeshRenderer1.materials = mats;
        mats = BodyMeshRenderer2.materials;
        mats[0] = MaterialsArray[PlayerID];
        BodyMeshRenderer2.materials = mats;

        RingDecal.material = RingMaterialsArray[PlayerID];
        
        StartCoroutine(SpawnToPosition());
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(SpawnToPosition());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator SpawnToPosition()
    {
        footIK_AF.followTerrain = false;
        /*foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }*/
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = true;
        }
        /*foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }*/
        yield return new WaitForSeconds(0.5f);
        switch (PlayerID)
        {
            case 0:
                playerMove.transform.position = stageInfo.SpawnPosition[0];
                playerMove.transform.eulerAngles = stageInfo.SpawnRotation[0];
                break;
            case 1:
                playerMove.transform.position = stageInfo.SpawnPosition[1];
                playerMove.transform.eulerAngles = stageInfo.SpawnRotation[1];
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
        }
        footIK_AF.followTerrain = true;
        /*foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }*/
        /*foreach (Rigidbody rb in rbs)
        {
            rb.constraints = RigidbodyConstraints.None;
        }*/
        yield return null;
    }
}
