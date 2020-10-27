using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AnimFollow;

// This scripts is controlled by StageManager. Deal with Scene change, win scene animation and so on. 
public class PlayerIdentity : MonoBehaviour
{
    [SerializeField]
    Material[] MaterialsArray;
    [SerializeField]
    Material[] RingMaterialsArray;
    /*[SerializeField]
    DecalProjector RingDecal;*/
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

    public GameObject[] Helmet;
    public GameObject Crown;

    public SkinnedMeshRenderer BodyMeshRenderer1;
    public SkinnedMeshRenderer BodyMeshRenderer2;
    public GameObject Decal;
    public GameObject RespawnPortal;

    PlayerCreating playerCreating;

    [SerializeField]
    Rigidbody[] rbs;
    Collider[] colliders = new Collider[0];

    // 亂寫的 要改
    PlayerInput playerInput;
    PlayerMove _playerMove;
    PlayerBehavior _playerBehavior;
    // 亂寫的 要改

    Coroutine respawn = null;

    public int PlayerID;

    private void Awake()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
        if(playerInput != null) PlayerID = playerInput.user.index;

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

        // RingDecal.material = RingMaterialsArray[PlayerID];

        /*_playerMove = GetComponentInChildren<PlayerMove>();
        _playerMove.enabled = false;
        _playerBehavior = GetComponentInChildren<PlayerBehavior>();
        _playerBehavior.enabled = false;*/
        mats = Decal.GetComponent<MeshRenderer>().materials;
        mats[0] = RingMaterialsArray[PlayerID];
        Decal.GetComponent<MeshRenderer>().materials = mats;

        StartCoroutine(SpawnToPosition());
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    /*private void OnLevelWasLoaded(int level)
    {
        Debug.Log("XD");
        if (level == 2) return;
        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
        StartCoroutine(SpawnToPositionLoad());
    }*/

    public void OnTest()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void InputCancel()
    {
        // playerInput.enabled = false;
    }

    public void InputEnable()
    {
        StartCoroutine(_InputEnable());
    }

    IEnumerator _InputEnable()
    {
        yield return new WaitForFixedUpdate();
        playerInput.enabled = true;
        // _playerMove.enabled = true;
        // _playerBehavior.enabled = true;
        yield return null;
    }

    public void SetKing()
    {
        for(int i = 0; i < Helmet.Length; i++)
        {
            Helmet[i].SetActive(false);
        }
        Crown.SetActive(true);
    }

    public void SetToAnimationMode()
    {
        // playerInput.enabled = false;
        // _playerMove.enabled = false;
        Decal.SetActive(false);
        playerMove.localScale = new Vector3(2.6f, 2.6f, 2.6f);
        // _playerBehavior.enabled = false;
        playerRig.gameObject.SetActive(false);
        // BodyMeshRenderer2.enabled = true;

        /*MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].enabled = true;
        }*/

        footIK_AF.enabled = false;
    }
    public void SetToPosition(Vector3 p)
    {
        playerMove.localPosition = p;
    }
    public void SetToRotation(Vector3 r)
    {
        playerMove.eulerAngles = r;
    }

    public void SetRagData()
    {
        Destroy(playerCreating);
        StartCoroutine(_SetRagData());
    }

    public void SetPlayerMaterial(int num, string name, float value)
    {
        Material[] mats = BodyMeshRenderer1.materials;
        mats[num].SetFloat(name, value);
    }

    IEnumerator _SetRagData()
    {
        Decal.SetActive(true);
        playerRigHips.transform.position = playerMove.transform.position;
        playerRigHips.transform.eulerAngles = playerMove.transform.eulerAngles;
        yield return new WaitForFixedUpdate();
        playerRig.gameObject.SetActive(true);
        colliders = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();
        yield return null;
    }

    public void Respawn()
    {
        if(respawn == null)
        {
            respawn = StartCoroutine(SpawnToPositionOnDeath());
        }
    }

    IEnumerator SpawnToPositionOnDeath()
    {
        foreach (Rigidbody rb in rbs)
        {
            if (rb == null) continue;
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            if (collider == null) continue;
            collider.isTrigger = true;
        }
        footIK_AF.followTerrain = false;
        yield return new WaitForFixedUpdate();

        playerMove.transform.position = stageInfo.SpawnPosition[PlayerID] + new Vector3(0,0,0);
        playerMove.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID] + new Vector3(0, 0, 0);
        playerRigHips.transform.position = stageInfo.SpawnPosition[PlayerID] + new Vector3(0, -3, 0);
        playerRigHips.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID] + new Vector3(0, -3, 0);

        SetPlayerMaterial(1, "_Transparent_Multiplier", 1);

        Destroy(Instantiate(RespawnPortal, stageInfo.SpawnPosition[PlayerID] + new Vector3(0, 0.1f, 0), Quaternion.Euler(90,0,0)), 2.5f);

        GlobalAudioPlayer.PlayRespawn();

        yield return new WaitForFixedUpdate();
        
        foreach (Rigidbody rb in rbs)
        {
            if (rb == null) continue;
            rb.AddForce(new Vector3(0, 1750, 0));
        }

        yield return new WaitForSeconds(1f);

        foreach (Rigidbody rb in rbs)
        {
            if (rb == null) continue;
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            if (collider == null) continue;
            collider.isTrigger = false;
        }
        footIK_AF.followTerrain = true;
        respawn = null;

        SetPlayerMaterial(1, "_Transparent_Multiplier", 0);

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

        // BodyMeshRenderer2.enabled = true;
        playerCreating.Creat();

        yield return null;
    }
}
