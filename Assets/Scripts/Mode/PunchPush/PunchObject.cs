using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PunchObject : MonoBehaviour
{
    [SerializeField]
    float Force;
    [SerializeField]
    float PunchDistance;

    [SerializeField]
    bool Firing = false;

    Coroutine c;

    PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
        c = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When get fire input. Set working coroutine. Coroutine has two state "Fire" and "Back". Impossible coexist. But it is possible to be null.

    public void Fire()
    {
        if (Firing) return;
        c = StartCoroutine(PunchEvent());
    }

    public void Back()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        // If collide anything object need set coroutine to "Back". If collision is player. It also need to add force to player's rigidbody.

        if(collision.gameObject.tag == "Player")
        {
            StopCoroutine(c);
            float distance = Vector3.Distance(new Vector3(0.9f, 0, 0), transform.localPosition);
            distance = Mathf.Min(distance, 2);
            transform.DOKill();
            c = StartCoroutine(BackEvent());
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(-(transform.position - collision.gameObject.transform.position).normalized * (Force / 4 * 100 * (distance + 2)));
        }
        else
        {
            StopCoroutine(c);
            transform.DOKill();
            c = StartCoroutine(BackEvent());
        }
    }

    // ！！！ If you didn't touch anything this Enumerator won't work. Bad design I know. ！！！

    IEnumerator BackEvent()
    {
        float distance = Vector3.Distance(new Vector3(0.9f, 0, 0), transform.localPosition);
        transform.DOLocalMove(new Vector3(0.9f, 0, 0), distance / 5.71f);
        yield return new WaitForSecondsRealtime(distance / 5.71f);
        playerMove.SetMoveEnable(true);
        Firing = false;
        yield return null;
    }

    IEnumerator PunchEvent()
    {
        playerMove.SetMoveEnable(false);
        Firing = true;
        transform.DOMove(transform.position + (PunchDistance * transform.forward), 0.4f).SetEase(Ease.InCirc);
        yield return new WaitForSecondsRealtime(0.4f);
        transform.DOLocalMove(new Vector3(0.9f, 0, 0), 0.7f);
        yield return new WaitForSecondsRealtime(0.7f);
        playerMove.SetMoveEnable(true);
        Firing = false;
        yield return null;
    }
}
