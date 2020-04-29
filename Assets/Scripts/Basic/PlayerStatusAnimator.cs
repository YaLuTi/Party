using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatusAnimator : MonoBehaviour
{
    public event EventHandler<StatusEventArgs> StatusUpdateHandler;
    List<StatusEventArgs> status = new List<StatusEventArgs>();

    Animator animator;

    float MoveSpeed;
    PlayerMove playerMove;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();
    }

    void OnTest()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }
    }

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", MoveSpeed);
    }

    public void MoveSpeedUpdate(float speed)
    {
        MoveSpeed = speed;
    }
    public void PlayerItem_Aim()
    {
        animator.SetTrigger("Throw1");
        playerMove.SetMoveEnable(false);
    }
    public void PlayerItem_Throw()
    {
        animator.SetTrigger("Throw2");
        playerMove.SetMoveEnable(true);
    }

    public void PlayerItemAnimation(string s)
    {
        animator.SetTrigger(s);
    }

    public void GetStatus(StatusEventArgs status, float time)
    {
        StatusUpdateHandler(this, status);
        StartCoroutine(_RemoveStatus(status, time));
    }
    IEnumerator _RemoveStatus(StatusEventArgs statusEvent ,float time)
    {
        status.Add(statusEvent);
        yield return new WaitForSecondsRealtime(time);
        status.Remove(statusEvent);

        yield return null;
    }

    public void RemoveStatus()
    {

    }
}

public class StatusEventArgs : EventArgs
{
    public int StatusID;
    public string StatusName;

    public StatusEventArgs(int _ID, string _StatusName)
    {
        StatusID = _ID;
        StatusName = _StatusName;
    }
}
