using AnimFollow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHitten : MonoBehaviour
{
    [SerializeField]
    RagdollControl_AF ragdollControl;

    PlayerBehavior pickItem;
    PlayerMove playerMove;

    public bool test;
    public Transform Hips;
    public Transform foot;
    public Transform FaceWay;
    public GameObject Decal;

    [Header("Health & UI")]
    [SerializeField]
    float MaxHealth;
    public float GetMaxHealth()
    {
        return MaxHealth;
    }

    public static int TestHP = 0;

    float Health;
    bool Dead = false;
    bool Respawnable = true;
    public void SetRespawnable(bool b)
    {
        Respawnable = b;
    }

    [SerializeField]
    GameObject PlayerUI;
    GameObject UI_copy;

    public delegate void PlayerHealthChangedHandler(PlayerHitten source, float oldHealth, float newHealth);
    public event PlayerHealthChangedHandler OnHealthChanged;

    public delegate void PlayerDeathHandler(PlayerHitten source);
    public event PlayerDeathHandler OnDeath;

    public FlagScore Flag;
    // Start is called before the first frame update
    void Start()
    {
        pickItem = GetComponentInChildren<PlayerBehavior>();
        playerMove = GetComponentInChildren<PlayerMove>();

        Health = MaxHealth;
        Dead = false;
        Respawnable = true;

        UI_copy = Instantiate(PlayerUI);
        UI_copy.GetComponent<PlayerUI>().SetUp(this);

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas != null)
        {
            UI_copy.transform.parent = canvas.transform;
            UI_copy.transform.localScale = new Vector3(1, 1, 1);
            switch (TestHP)
            {
                case 0:
                    UI_copy.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270, 170);
                    break;
                case 1:
                    UI_copy.GetComponent<RectTransform>().anchoredPosition = new Vector2(270, 170);
                    break;
                case 2:
                    UI_copy.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270, -170);
                    break;
                case 3:
                    UI_copy.GetComponent<RectTransform>().anchoredPosition = new Vector2(270, -170);
                    break;
            }
            TestHP++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Decal.transform.position = Hips.transform.position;
        Vector3 v = Decal.transform.position;
        v.y = foot.position.y;
        Decal.transform.position = v;
    }
    public void OnHit(BulletHitInfo_AF info)
    {
        if (Flag != null) Flag.Throw();
        Flag = null;


        StartCoroutine(AddForceToLimb(info));

        if (ragdollControl.shotByBullet) return;

        if (info.IsShot)
        {
            ragdollControl.shotByBullet = true;
        }
        else
        {

        }
        pickItem.OnHit(info);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForFixedUpdate();
        ragdollControl.shotByBullet = true;
        ragdollControl.IsDead = true;
        pickItem.OnHit();
        yield return new WaitForSeconds(2f);
        if (Respawnable)
        {
            GetComponent<PlayerIdentity>().Respawn();
            Health = MaxHealth;
            OnHealthChanged?.Invoke(this, 0, Health);
            ragdollControl.IsDead = false;
            Dead = false;
        }
        yield return null;
    }

    public void OnDamaged(float damage)
    {
        if (ragdollControl.shotByBullet || Dead) return;

        float oldH = Health;
        Health -= damage;
        Health = Mathf.Max(0, Health);
        if (Health == 0)
        {
            if (Flag != null) Flag.Throw();
            Flag = null;
            StartCoroutine(Respawn());
            OnDeath?.Invoke(this);
            Dead = true;
        }

        OnHealthChanged?.Invoke(this, oldH, Health);
    }

    public bool IsGettingUp()
    {
        return ragdollControl.PlayerInhibit();
    }

    IEnumerator AddForceToLimb(BulletHitInfo_AF bulletHitInfo)
    {
        yield return new WaitForFixedUpdate();
        if (bulletHitInfo.hitPoint != null)
        {
            bulletHitInfo.hitTransform.GetComponent<Rigidbody>().AddForceAtPosition(bulletHitInfo.bulletForce, bulletHitInfo.hitPoint);
        }
        playerMove.AddForceSpeed(bulletHitInfo.bulletForce / 100f);
    }
}
