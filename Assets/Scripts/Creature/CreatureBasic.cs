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
    protected float speed;
    [SerializeField]
    protected float RotateSpeed;
    protected NavMeshAgent nav;
    protected Animator animator;

    GameObject[] players;
    protected Vector3 following;
    [SerializeField]
    protected LayerMask layerMask;
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
    }

    public virtual void Death()
    {
    }
    

}
