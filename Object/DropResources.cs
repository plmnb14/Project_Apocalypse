using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropResources : WorldObject
{
    public long value { get; set; }
    public Vector3 targetVector { get; set; }
    public LayerMask targetMask { get; set; }

    private float lifeTime;
    private Rigidbody2D rigid;
    private Collider2D coll;

    protected override void OnDie()
    {
        base.OnDie();

        value = System.Convert.ToInt64(Random.Range(1000, 3000));
        if(myName == "UI_DropCoin")
        {
            ResourceManager.instance.Coin += value;

            Camera tmpCam = Camera.main;
            RectTransform rect = GameObject.Find("Coin_Icon").transform.GetComponent<RectTransform>();

            VFX vfx = VFXManager.instance.GetVFX("VFX_Hit_01");
            vfx.transform.position = tmpCam.ScreenToWorldPoint(new Vector2(rect.position.x, rect.position.y));
        }
        else if (myName == "UI_DropCrystal")
        {
            ResourceManager.instance.CrystalFree += value;
        }
        else if (myName == "UI_DropElement")
        {
            ResourceManager.instance.Element += value;
        }


        ResourceManager.instance.BackResource(myName, this);
    }

    public override void ResetStatus(Transform parent = null)
    {
        coll.isTrigger = false;
        rigid.gravityScale = 1.0f;
        rigid.velocity = Vector3.zero;

        base.ResetStatus();
    }

    private void OnFly()
    {
        float value = Random.Range(0.35f, 0.5f);
        transform.DOMove(targetVector, value).SetEase(Ease.InQuad).OnComplete
            (() => { OnDie(); });

    }

    private IEnumerator OnCreate()
    {
        float rand = Random.Range(70.0f, 110.0f);
        float power = Random.Range(5.0f, 8.0f);

        Quaternion q = Quaternion.Euler(0.0f, rand, 0.0f);
        Vector2 direction = (q * Vector2.one) * power;

        rigid.AddForce(direction, ForceMode2D.Impulse);

        yield return new WaitForSeconds(lifeTime);

        coll.isTrigger = true;
        rigid.gravityScale = 0.0f;
        rigid.velocity = Vector3.zero;

        OnFly();

        yield break;
    }

    private void SetUp()
    {
        value = 0;
        lifeTime = 1.0f;

        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(OnCreate());
    }

    private void Awake()
    {
        SetUp();
    }
}
