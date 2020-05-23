using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AnimFollow;

[RequireComponent(typeof(NavMeshAgent))]
public class CreatureBasic : MonoBehaviour
{
    [SerializeField]
    float LifeTime;
    NavMeshAgent nav;

    GameObject[] players; 
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Destroy(this.gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(FlagScore.id < 0)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0) return;
            GameObject cloest = null;
            float distance = 100;
            for (int i = 0; i < players.Length; i++)
            {
                PlayerHitten playerHitten = players[i].GetComponent<PlayerHitten>();
                float d = Vector3.Distance(transform.position, playerHitten.Hips.position);
                if (d < distance)
                {
                    cloest = players[i];
                    distance = d;
                }
            }
            nav.SetDestination(cloest.GetComponent<PlayerHitten>().Hips.position);
        }
        else
        {
            nav.SetDestination(FlagScore.follow.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Debug.Log(2);
            BulletHitInfo_AF bulletHitInfo_AF = new BulletHitInfo_AF();
            bulletHitInfo_AF.bulletForce = Vector3.zero;
            // bulletHitInfo.hitNormal = raycastHit.normal;
            bulletHitInfo_AF.hitTransform = other.transform;
            bulletHitInfo_AF.hitPoint = other.ClosestPoint(transform.position);
            other.transform.root.gameObject.GetComponent<PlayerHitten>().OnHit(bulletHitInfo_AF);
            Destroy(this.gameObject);
        }
    }
    
}
