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
    [SerializeField]
    float speed;
    [SerializeField]
    Collider mainCollider;
    NavMeshAgent nav;
    Animator animator;

    GameObject[] players; 
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // Destroy(this.gameObject, LifeTime);
        animator = GetComponent<Animator>();
        nav.avoidancePriority = (int)Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        {
            /*if(FlagScore.id < 0)
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
            }*/
        }
        for(int i = 0; i < StageManager.players.Count; i++)
        {
            nav.SetDestination(StageManager.players[i].GetComponent<PlayerHitten>().Hips.position);
        }
        /*if(Time.time > 5)
        {
            Death();
        }*/
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
            // other.transform.root.gameObject.GetComponent<PlayerHitten>().OnDamaged(5);
            Destroy(this.gameObject);
        }
    }

    public void Death()
    {
        animator.enabled = false;
        nav.enabled = false;
        mainCollider.enabled = false;
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            if (c != mainCollider)
            {
                // c.GetComponent<Rigidbody>().velocity = Vector3.zero;
                c.enabled = true;
            }
        }
        this.enabled = false;
        Destroy(this.gameObject, 4f);
    }

    IEnumerator _Death()
    {
        yield return new WaitForFixedUpdate();
        yield return null;
    }

    private void OnAnimatorMove()
    {
        if (animator == null) return;
        nav.velocity = animator.deltaPosition / Time.deltaTime * speed;

        Vector3 dir = nav.desiredVelocity;
        dir.y = 0f;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
