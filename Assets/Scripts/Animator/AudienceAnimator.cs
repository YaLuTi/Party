using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceAnimator : MonoBehaviour
{
    Animator animator;
    SkinnedMeshRenderer BodyMeshRenderer1;
    [SerializeField]
    Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.6f, 1.3f);
        BodyMeshRenderer1 = GetComponentInChildren<SkinnedMeshRenderer>();
        Material[] mats = BodyMeshRenderer1.materials;
        mats[0] = materials[(int)Random.Range(0,materials.Length)];
        BodyMeshRenderer1.materials = mats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string s)
    {
        animator.SetTrigger(s);
    }
}
