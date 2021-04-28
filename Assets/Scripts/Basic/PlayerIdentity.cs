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

    public int Helmetnum;

    public SkinnedMeshRenderer BodyMeshRenderer1;
    public SkinnedMeshRenderer BodyMeshRenderer2;
    public SkinnedMeshRenderer EffectMeshRenderer;
    public GameObject Decal;
    public GameObject RespawnPortal;

    PlayerCreating playerCreating;

    [SerializeField]
    Rigidbody[] rbs;
    Collider[] colliders = new Collider[0];

    PlayerInput playerInput;

    // 亂寫的 要改
    PlayerMove _playerMove;
    PlayerBehavior _playerBehavior;
    // 亂寫的 要改

    Coroutine respawn = null;

    [SerializeField]
    bool IsCPU = false;

    public int PlayerID;

    private void Awake()
    {
        playerInput = GetComponentInChildren<PlayerInput>();
        if(playerInput != null) PlayerID = playerInput.user.index;

        playerCreating = GetComponentInChildren<PlayerCreating>();

        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();

        colliders = GetComponentsInChildren<Collider>();
        rbs = GetComponentsInChildren<Rigidbody>();

        if (IsCPU) return;
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
        Material material = Decal.GetComponent<Projector>().material;
        material = RingMaterialsArray[PlayerID];
        Decal.GetComponent<Projector>().material = material;

        if (StageManager.Static_Testing)
        {
            StartCoroutine(SpawnToPosition());
        }
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
        playerInput.SwitchCurrentActionMap("None");
    }

    public void InputUI()
    {
            playerInput.SwitchCurrentActionMap("Creating(UI)");
    }

    public void InputEnable()
    {
        playerInput.SwitchCurrentActionMap("GamePlay");
    }

    public void PlayerInputEnable()
    {
        // playerInput.SwitchCurrentActionMap("GamePlay");
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
        /*
        // playerInput.enabled = false;
        // _playerMove.enabled = false;
        Decal.SetActive(false);
        playerMove.localScale = new Vector3(2.6f, 2.6f, 2.6f);
        // _playerBehavior.enabled = false;
        playerRig.gameObject.SetActive(false);
        // BodyMeshRenderer2.enabled = true;

        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        for(int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].enabled = true;
        }

        footIK_AF.enabled = false;*/

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
        // Destroy(playerCreating);
        StartCoroutine(_SetRagData());
    }

    public void SetPlayerMaterial(int num, string name, float value)
    {
        EffectMeshRenderer.gameObject.SetActive(true); // x
        Material[] mats = EffectMeshRenderer.materials;
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

    public void ResetStageData()
    {
        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
        StartCoroutine(SpawnToPositionLoad());
    }

    public void Respawn()
    {
        if(respawn == null)
        {
            respawn = StartCoroutine(SpawnToPositionOnDeath());
        }
    }

    public void Freeze()
    {
        footIK_AF.followTerrain = false;
        playerInput.SwitchCurrentActionMap("None");
        foreach (Rigidbody rb in rbs)
        {
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }
        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                collider.isTrigger = true;
            }
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

        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
        playerMove.transform.position = stageInfo.SpawnPosition[PlayerID] + new Vector3(0,0,0);
        playerMove.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID] + new Vector3(0, 0, 0);
        playerRigHips.transform.position = stageInfo.SpawnPosition[PlayerID] + new Vector3(0, -3, 0);
        playerRigHips.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID] + new Vector3(0, -3, 0);

        Destroy(Instantiate(RespawnPortal, stageInfo.SpawnPosition[PlayerID] + new Vector3(0, 0.1f, 0), Quaternion.Euler(-90,0,0)), 8f);

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
        if (!StageManager.InLobby && !SceneChangeTest.IsLoadingTutorial)
        {
            footIK_AF.followTerrain = true;
        }
        respawn = null;

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

        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
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
        if (!StageManager.InLobby && !SceneChangeTest.IsLoadingTutorial)
        {
            footIK_AF.followTerrain = true;
        }
        yield return null;
    }

    public void SetToPosition()
    {
        StartCoroutine(SpawnToPosition());
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

        stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
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

        if (!StageManager.InLobby && !SceneChangeTest.IsLoadingTutorial)
        {
            footIK_AF.followTerrain = true;
        }

        yield return new WaitForFixedUpdate();
        SetRagData();
        yield return new WaitForFixedUpdate();
        // BodyMeshRenderer2.enabled = true;
        playerCreating.Creat();

        yield return null;
    }

    public void SetToTransform()
    {
        StartCoroutine(SpawnToTransform());
    }
    IEnumerator SpawnToTransform()
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

        if (GameObject.FindGameObjectWithTag("StageInfoTransform") == null)
        {
            playerMove.transform.position = stageInfo.SpawnPosition[PlayerID];
            playerMove.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID];
            playerRigHips.transform.position = stageInfo.SpawnPosition[PlayerID];
            playerRigHips.transform.eulerAngles = stageInfo.SpawnRotation[PlayerID];
        }
        else
        {
            StageInfoTransform stageInfoTransform = GameObject.FindGameObjectWithTag("StageInfoTransform").GetComponent<StageInfoTransform>();
            playerMove.transform.position = stageInfoTransform.transforms[PlayerID].position;
            playerMove.transform.eulerAngles = stageInfoTransform.transforms[PlayerID].eulerAngles;
            playerRigHips.transform.position = stageInfoTransform.transforms[PlayerID].position;
            playerRigHips.transform.eulerAngles = stageInfoTransform.transforms[PlayerID].eulerAngles;
        }


        yield return new WaitForFixedUpdate();
        foreach (Rigidbody rb in rbs)
        {
            rb.velocity = Vector3.zero;
        }
        foreach (Collider collider in colliders)
        {
            collider.isTrigger = false;
        }

        if (!StageManager.InLobby && !SceneChangeTest.IsLoadingTutorial)
        {
            footIK_AF.followTerrain = true;
        }

        yield return new WaitForFixedUpdate();
        SetRagData();
        yield return new WaitForFixedUpdate();
        // BodyMeshRenderer2.enabled = true;
        playerCreating.Creat();

        yield return null;
    }
}
