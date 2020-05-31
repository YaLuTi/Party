using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using AnimFollow;

// This scripts is controlled by StageManager. Deal with Scene change, win scene animation and so on. 
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
    Transform playerRigHips;
    [SerializeField]
    SimpleFootIK_AF footIK_AF;
    StageInfo stageInfo;
    [SerializeField]
    Vector3[] SpawnPosition;
    [SerializeField]
    Vector3[] SpawnRotation;

    public SkinnedMeshRenderer BodyMeshRenderer1;
    public SkinnedMeshRenderer BodyMeshRenderer2;

    PlayerCreating playerCreating;

    [SerializeField]
    Rigidbody[] rbs;
    Collider[] colliders = new Collider[0];

    // 亂寫的 要改
    PlayerInput playerInput;
    PlayerMove _playerMove;
    PlayerBehavior _playerBehavior;
    // 亂寫的 要改

    public int PlayerID;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
        PlayerID = playerInput.user.index;

        playerCreating = GetComponentInChildren<PlayerCreating>();

        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
        
        colliders = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();

        Material[] mats = BodyMeshRenderer1.materials;
        mats[0] = MaterialsArray[PlayerID];
        BodyMeshRenderer1.materials = mats;
        mats = BodyMeshRenderer2.materials;
        mats[0] = MaterialsArray[PlayerID];
        BodyMeshRenderer2.materials = mats;

        RingDecal.material = RingMaterialsArray[PlayerID];

        _playerMove = GetComponentInChildren<PlayerMove>();
        _playerMove.enabled = false;
        _playerBehavior = GetComponentInChildren<PlayerBehavior>();
        _playerBehavior.enabled = false;

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

    public void InputCancel()
    {
        playerInput.enabled = false;
    }

    public void InputEnable()
    {
        Debug.Log("A");
        playerInput.enabled = true;
        _playerMove.enabled = true;
        _playerBehavior.enabled = true;
    }

    public void SetRagData()
    {
        Destroy(playerCreating);
        StartCoroutine(_SetRagData());
    }

    IEnumerator _SetRagData()
    {
        playerRigHips.transform.position = playerMove.transform.position;
        playerRigHips.transform.eulerAngles = playerMove.transform.eulerAngles;
        yield return new WaitForFixedUpdate();
        playerRig.gameObject.SetActive(true);
        colliders = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();
        yield return null;
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

        playerMove.transform.position = stageInfo.SpawnPosition[PlayerID];
        playerMove.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID];
        playerRigHips.transform.position = stageInfo.SpawnPosition[PlayerID];
        playerRigHips.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID];

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
        yield return new WaitForFixedUpdate();

        playerMove.transform.position = stageInfo.SpawnPosition[PlayerID];
        playerMove.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID];
        playerRigHips.transform.position = stageInfo.SpawnPosition[PlayerID];
        playerRigHips.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID];

        yield return new WaitForFixedUpdate();
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
        playerCreating.Creat();

        yield return null;
    }
}
