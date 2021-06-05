using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarningBullet : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    int PlayerID;
    [SerializeField]
    float delay;
    [SerializeField]
    float DestroyDelay;
    [SerializeField]
    float WarningSpeed;

    Projector projector;
    [SerializeField]
    float ProjectorSize;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        projector = GetComponent<Projector>();
        StartCoroutine(Delay());
        Destroy(this.gameObject, DestroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        t += WarningSpeed * Time.deltaTime;
        projector.orthographicSize = Mathf.Lerp(0.5f, ProjectorSize, t);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        Fire(0);
    }

    RFX4_PhysicsMotion Fire(float angle)
    {
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        b.transform.eulerAngles += new Vector3(0, angle, 0);
        Destroy(b, 6);
        if (b.GetComponentInChildren<RFX4_PhysicsMotion>() != null)
        {
            b.GetComponentInChildren<RFX4_PhysicsMotion>().PlayerID = PlayerID;
            return b.GetComponentInChildren<RFX4_PhysicsMotion>();
        }
        if (b.GetComponentInChildren<Basic_Bullet>() != null)
        {
            b.GetComponentInChildren<Basic_Bullet>().PlayerID = PlayerID;
            return null;
        }
        return null;
    }
}
