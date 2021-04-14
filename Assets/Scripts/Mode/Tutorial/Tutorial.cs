using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public int num = 0;

    [SerializeField]
    CinemachineTargetGroup CineGroup;
    [SerializeField]
    GameObject Teacher;

    [SerializeField]
    GameObject bomb;
    [SerializeField]
    FacilityArea MoveArea;

    [SerializeField]
    GameObject FireBallSpawn;

    [SerializeField]
    PlayerHitten Doll;

    [SerializeField]
    TMP_Animated textMesh;

    Coroutine coroutine;
    [SerializeField]
    AudioSource voiceSource;
    [SerializeField]
    AudioClip[] voices;


    // Start is called before the first frame update
    void Start()
    {
        CineGroup.AddMember(Teacher.transform, 1, 0);
        textMesh.ReadText("Come here");
    }

    // Update is called once per frame
    void Update()
    {
        switch (num)
        {
            // Move to master
            case 0:
                if(StageManager.players.Count == MoveArea.PlayersNum && MoveArea.PlayersNum != 0)
                {
                    num++;
                    coroutine = StartCoroutine(Step1());
                }
                break;
            // Use Fire ball kill enemy
            case 1:
                if (Doll.Dead)
                {
                    num++;
                    coroutine = StartCoroutine(Step2());
                }
                break;
            // Use Bomb kill enemy
            case 2:
                if (Doll.Dead)
                {
                    num++;
                }
                break;
            default:
                break;
        }
    }

    void PlaySound()
    {
        voiceSource.clip = voices[Random.Range(0, voices.Length)];
        voiceSource.Play();
    }

    IEnumerator Step1()
    {
        textMesh.ReadText("Great! Now we are going to learn how to use magic.");
        PlaySound();
        yield return new WaitForSeconds(3.5f);

        textMesh.ReadText("I will creat fire magic book and doll.");
        PlaySound();
        yield return new WaitForSeconds(3.5f);

        FireBallSpawn.SetActive(true);
        Doll.gameObject.SetActive(true);
        textMesh.ReadText("Now use your magic destroy that son of bitch.");
        PlaySound();
        yield return new WaitForSeconds(4f);

        yield return null;
    }

    IEnumerator Step2()
    {
        textMesh.ReadText("Well Done!");
        PlaySound();
        yield return new WaitForSeconds(2f);

        textMesh.ReadText("There will be lots of different magic book.");
        PlaySound();
        yield return new WaitForSeconds(3.5f);

        textMesh.ReadText("But they are used in same way.");
        PlaySound();
        yield return new WaitForSeconds(3f);

        textMesh.ReadText("Now let study how to use bomb.");
        PlaySound();
        yield return new WaitForSeconds(3f);
        yield return null;
    }
}
