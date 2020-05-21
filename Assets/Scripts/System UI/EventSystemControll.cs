using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystemControll : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject FirstSelected;
    // Start is called before the first frame update

    private void OnEnable()
    {
        StartCoroutine(Select());
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Select()
    {
        eventSystem.SetSelectedGameObject(FirstSelected);
        yield return null;
    }
}
