using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Bullet : Enemy
{
    [SerializeField]
    private GameObject bigBullet;
    private SpriteRenderer spriteRenderer;
    private Boss boss;
    private Transform bossTransform;
    private void Start()
    {
        Coefficient = 1.0f;
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss>();
        bossTransform = GameObject.FindWithTag("Boss").GetComponent<Transform>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boss.Charged();
        StartCoroutine(Excute());
    }

    private IEnumerator Excute()
    {
        yield return YieldInstructionCache.WaitingSecond(0.25f);
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer,0.5f));
        spriteRenderer.enabled = false;
        GameObject bullet = Instantiate(bigBullet,bossTransform.position,Quaternion.identity);
        bullet.GetComponent<Big_Ball>().SetPos(bossTransform.position, gameObject.transform.position);
        boss.ChargeDown();
        Destroy(gameObject, 1.0f);
    }
}
