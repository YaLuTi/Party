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

    [SerializeField]
    Rigidbody[] rbs;
    Collider[] colliders = new Collider[0];

    PlayerInput playerInput;
    public int PlayerID;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
        PlayerID = playerInput.user.index;

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
        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
        StartCoroutine(SpawnToPositionLoad());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetRagData()
    {
        playerRig.gameObject.SetActive(true);
        colliders = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    IEnumerator SpawnToPositionLoad()
    {
        footIK_AF.followTerrain = false;
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = true;
        }
        yield return new WaitForFixedUpdate();
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
        yield return new WaitForSeconds(1.5f);
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
        }
        footIK_AF.followTerrain = true;
        yield return null;
    }
    IEnumerator SpawnToPosition()
    {
        footIK_AF.followTerrain = false;
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = true;
        }
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
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
        }
        footIK_AF.followTerrain = true;
        yield return new WaitForFixedUpdate();
        BodyMeshRenderer2.enabled = true;
        yield return null;
    }
}
