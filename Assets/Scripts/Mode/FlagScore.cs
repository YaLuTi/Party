using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlagScore : MonoBehaviour
{
    [SerializeField]
    Vector3 spawnPosition;
    [SerializeField]
    LayerMask PlayerLayerMask;
    [SerializeField]
    LayerMask GroundLayerMask;
    [SerializeField]
    float RotateSpeed;

    float g = 0;

    public static int id = -1;
    public static Transform follow;
    
    public bool IsHolded = false;
    float cooldown = 0;

    public int RespawnTime = 30;

    RoamingAI roamingAI;
    // Start is called before the first frame update
    void Start()
    {
        StageInfo stageInfo = GameObject.FindGameObjectWithTag("StageInfo").GetComponent<StageInfo>();
        Vector3 v = new Vector3();
        for(int i = 0; i < stageInfo.SpawnPosition.Count; i++)
        {
            v += stageInfo.SpawnPosition[i];
        }
        transform.position = v / stageInfo.SpawnPosition.Count;
        transform.position += new Vector3(0, 3, 0);
        spawnPosition = transform.position;

        roamingAI = GetComponent<RoamingAI>();
        InvokeRepeating("RespawnEvent", RespawnTime, RespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, RotateSpeed, 0) * Time.deltaTime;

        if (transform.parent != null)
        {
            transform.position = follow.position + new Vector3(0, 2, 0);
            if (cooldown > 45)
            {
                cooldown = 0;
                ScoreManager.AddScore(id, 1);
            }
            cooldown += 60 * Time.deltaTime;
        }
        else
        {
            cooldown = 0;
        }


        if (!IsHolded)
        {
            // Gravity
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out raycastHit, 1.2f, GroundLayerMask))
            {
                g = 0;
                if (raycastHit.distance < 1.1f)
                {
                    transform.position += new Vector3(0, 0.5f, 0) * Time.deltaTime;
                }
            }
            else
            {
                g += 0.1f;
                transform.position -= new Vector3(0, g, 0) * Time.deltaTime;
            }

            // Get Player
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1, PlayerLayerMask);
            foreach (Collider collider in colliders)
            {
                if ((PlayerLayerMask.value & 1 << collider.gameObject.layer) > 0)
                {
                    if (collider.transform.root.GetComponent<PlayerHitten>().IsGettingUp()) continue;
                    IsHolded = true;
                    transform.parent = collider.transform.root;
                    id = GetComponentInParent<PlayerIdentity>().PlayerID;
                    follow = GetComponentInParent<PlayerHitten>().Hips;
                    GetComponentInParent<PlayerHitten>().Flag = this;
                    break;
                }
            }
        }

        if (transform.position.y < -5 || transform.position.y > 15)
        {
            IsHolded = false;
            transform.parent = null;
            id = -1;
            follow = null;
            g = 0;
            Vector3 p = roamingAI.PickNewDestination();
            transform.localScale = Vector3.zero;
            transform.DOScale(1.5f, 1f);
            transform.position = p;
        }
    }

    void RespawnEvent()
    {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        transform.DOScale(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        IsHolded = false;
        transform.parent = null;
        id = -1;
        follow = null;
        g = 0;

        Vector3 p = roamingAI.PickNewDestination();
        transform.localScale = Vector3.zero;
        transform.position = p + new Vector3(0, 1, 0);

        yield return new WaitForSeconds(1);
        transform.DOScale(1.5f, 1f);

        yield return null;
    }

    public void Throw()
    {
        Debug.Log("????");
        IsHolded = false;
        transform.parent = null;
        id = -1;
        follow = null;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (IsHolded) return;
        if (other.transform.root.tag == "Player")
        {
            Debug.Log("B");
            IsHolded = true;
            transform.parent = other.transform.root;
            id = GetComponentInParent<PlayerIdentity>().PlayerID;
            follow = GetComponentInParent<PlayerHitten>().Hips;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (IsHolded) return;
        if (!StageManager.InGame) return;
        if (transform.localScale.x < 1.2f) return;
        if (collision.transform.root.tag == "Player")
        {
            Debug.Log("Shit");
            if (collision.transform.root.GetComponent<PlayerHitten>().Dead) return;
            IsHolded = true;
            transform.parent = collision.transform.root;
            id = GetComponentInParent<PlayerIdentity>().PlayerID;
            follow = GetComponentInParent<PlayerHitten>().Hips;
            GetComponentInParent<PlayerHitten>().Flag = this;
        }
    }
}
