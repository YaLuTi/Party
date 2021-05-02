using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimFollow;
using UnityEngine.AI;

public class CreatureZombie : CreatureBasic
{
    [SerializeField]
    Collider mainCollider;

    float cooldown;
    float o_Speed;
    float o_RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // Destroy(this.gameObject, LifeTime);
        animator = GetComponent<Animator>();
        nav.avoidancePriority = (int)Random.Range(0, 100);
        nav.speed = Random.Range(2, 4.5f);
        nav.angularSpeed = Random.Range(90, 270);
        InvokeRepeating("SetTarget", 0, 1);
        o_Speed = speed;
        o_RotateSpeed = RotateSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, following) < 7.5f && Vector3.Distance(transform.position, following) > 2f && cooldown == 0)
        {
            cooldown = 1;
            StartCoroutine(_Charge());
        }
    }
    IEnumerator _Charge()
    {
        animator.SetBool("Run", true);
        yield return new WaitForSeconds(1.5f);
        speed = 3f;
        RotateSpeed = 0.5f;
        while(speed > 0)
        {
            speed -= Time.deltaTime * 1.75f;
            yield return null;
        }
        speed = 0;
        animator.SetBool("Run", false);
        if (!animator.GetBool("Attack"))
        {
            yield return new WaitForSeconds(1);
        }
        speed = o_Speed;
        RotateSpeed = o_RotateSpeed;
        yield return new WaitForSeconds(2);
        cooldown = 0;
        yield return null;
    }

    void SetTarget()
    {
        GameObject cloest = null;
        float distance = 100;
        for (int i = 0; i < StageManager.players.Count; i++)
        {
            PlayerHitten playerHitten = StageManager.players[i].GetComponent<PlayerHitten>();
            float d = Vector3.Distance(transform.position, playerHitten.Hips.position);
            if (d < distance)
            {
                cloest = StageManager.players[i];
                distance = d;
            }
        }
        following = cloest.GetComponent<PlayerHitten>().Hips.position;
        nav.SetDestination(cloest.GetComponent<PlayerHitten>().Hips.position);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!((layerMask.value & 1 << collision.gameObject.layer) > 0)) return;
        collision.transform.root.GetComponent<PlayerHitten>().OnDamaged(0.01f, -1);
        speed = 0.3f;
        animator.SetBool("Attack", true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!((layerMask.value & 1 << collision.gameObject.layer) > 0)) return;
        animator.SetBool("Attack", false);
        speed = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            BulletHitInfo_AF bulletHitInfo_AF = new BulletHitInfo_AF();
            bulletHitInfo_AF.bulletForce = Vector3.zero;
            // bulletHitInfo.hitNormal = raycastHit.normal;
            bulletHitInfo_AF.hitTransform = collision.transform;
            bulletHitInfo_AF.hitPoint = collision.contacts[0].point;
            collision.transform.root.gameObject.GetComponent<PlayerHitten>().OnHit(bulletHitInfo_AF);
            // other.transform.root.gameObject.GetComponent<PlayerHitten>().OnDamaged(5);
            Destroy(this.gameObject);
        }
      */
    }

    public override void Death()
    {
        base.Death();
        animator.enabled = false;
        nav.enabled = false;
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            if (c != mainCollider)
            {
                // c.GetComponent<Rigidbody>().velocity = Vector3.zero;
                c.enabled = true;
            }
        }
        Destroy(this);
        mainCollider.enabled = false;
        Destroy(this.gameObject, 4f);
    }
    
    private void OnAnimatorMove()
    {
       if (animator == null) return;
       nav.velocity = animator.deltaPosition / Time.deltaTime * speed;

        var targetRotation = Quaternion.LookRotation(following - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotateSpeed * Time.deltaTime);
    }
    
}
