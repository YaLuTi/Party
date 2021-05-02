using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTDELTA : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnAnimatorMove()
    {
        transform.position += animator.deltaPosition;
        Debug.Log(animator.deltaPosition);
    }
}
