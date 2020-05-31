using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMine : ItemBasic
{
    protected bool IsSetted = false;
    [Header("MineValue")]
    [SerializeField]
    GameObject UnusedModel;
    [SerializeField]
    GameObject UsedModel;
    [Header("GameValue")]
    [SerializeField]
    LayerMask layerMask;
    public float delay;
    public float Radius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        if (!IsSetted) return;
        SettedEvent();
    }

    public virtual void SettedEvent()
    {

    }

    public override string OnUse()
    {
        if (DurabilityCheck())
        {
            UnusedModel.SetActive(false);
            UsedModel.SetActive(true);
            transform.parent = null;
            StartCoroutine(SetDelay());
        }
        else
        {
            return "Empty";
        }
        return base.OnUse();
    }

    IEnumerator SetDelay()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        yield return new WaitForSecondsRealtime(delay);
        IsSetted = true;
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnCollisionStay(Collision collision)
    {
    }
}
