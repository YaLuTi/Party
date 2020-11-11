using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityArea : MonoBehaviour
{
    public int PlayersNum = 0;
    protected bool IsNeedAll = false;
    public bool IsUsing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnUse(PlayerBehavior playerBehavior)
    {
        if (IsNeedAll && PlayersNum < StageManager.players.Count)
        {
            return;
        }
    }

    public virtual void OnCancel()
    {

    }
}
