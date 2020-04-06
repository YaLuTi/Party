using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStatus : MonoBehaviour
{
    public event EventHandler<StatusEventArgs> StatusUpdateHandler;
    List<StatusEventArgs> status = new List<StatusEventArgs>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
