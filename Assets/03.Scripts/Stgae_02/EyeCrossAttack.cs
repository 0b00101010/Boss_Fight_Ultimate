using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCrossAttack : Enemy
{
    [SerializeField]
    private Sprite[] eyeCrossesSprite;

    [SerializeField]
    private GameObject EyeCross;

    [SerializeField]
    private SpriteRenderer backGround;

    [SerializeField]
    private Sprite[] backgroundSpriters;

    private void Start()
    {
        Coefficient = 0.8f;
    }

    public void CrossAttackInit()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(EyeCross.GetComponent<SpriteRenderer>(), 0.35f));
    }

    public void CrossAttack()
    {
        StartCoroutine(Excute());
    }


    private IEnumerator Excute()
    {
        EyeCross.transform.position = new Vector3(Random.Range(-5.35f, 5.35f), Random.Range(0, 3f));
        SpriteRenderer spriteRenderer = EyeCross.GetComponent<SpriteRenderer>();
        backGround.sprite = backgroundSpriters[0];
        spriteRenderer.sprite = eyeCrossesSprite[1];
        EyeCross.tag = "Enemy";
        for(int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.15f);
            backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, backGround.color.a - 0.05f);
            yield return YieldInstructionCache.WaitingSecond(0.02f);
        }

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + 0.15f);
            backGround.color = new Color(backGround.color.r, backGround.color.g, backGround.color.b, backGround.color.a + 0.05f);
            yield return YieldInstructionCache.WaitingSecond(0.02f);
        }
        EyeCross.tag = "Untagged";
        backGround.sprite = backgroundSpriters[1];
        spriteRenderer.sprite = eyeCrossesSprite[0];

    }

    public void EndCrossAttack()
    {
        SpriteRenderer spriteRenderer = EyeCross.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(255,255,255,0);
    }
}
