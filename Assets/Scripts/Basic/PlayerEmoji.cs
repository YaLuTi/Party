using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmoji : MonoBehaviour
{
    public GameObject[] Emojis;
    public Transform SpawnTransform;
    public Transform FollowTransform;
    bool cooldown;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < Emojis.Length; i++)
        {
            Emojis[i].transform.position = FollowTransform.position + Offset;
        }*/
    }

    void OnPad_Down()
    {
        PlayEmoji(0);
    }

    void PlayEmoji(int i)
    {
        if (!cooldown)
        {
            Vector3 Offset = Emojis[i].transform.position;
            Vector3 r = Emojis[i].transform.eulerAngles;

            GameObject g = Instantiate(Emojis[i], SpawnTransform.position + Offset, Quaternion.identity);
            g.transform.parent = SpawnTransform.transform;

            g.transform.localEulerAngles = r;
            g.transform.localPosition = Offset;
            g.GetComponent<Animator>().SetTrigger("Play");
            Destroy(g, 2f);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Spawn()
    {
        yield return null;
    }

    IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(1.5f);
        cooldown = false;
        yield return null;
    }
}
