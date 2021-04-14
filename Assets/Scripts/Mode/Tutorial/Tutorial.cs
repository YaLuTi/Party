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

    IEnumerator Step1()
    {
        textMesh.ReadText("Great! Now we are going to learn how to use magic.");
        yield return new WaitForSeconds(4);
        textMesh.ReadText("I will creat fire magic book and doll.");
        yield return new WaitForSeconds(4);
        textMesh.ReadText("Now use your magic destroy that son of bitch.");
        yield return new WaitForSeconds(7);
        yield return null;
    }
}
