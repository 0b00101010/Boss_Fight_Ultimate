using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_LadyBug : StageBoss
{
    [SerializeField]
    private SpriteRenderer chargeSprite;
    [SerializeField]
    private SpriteRenderer darkenSprite;

    private void Awake()
    {
        Damage = 20.0f;
    }

    public void Charged() {
        StartCoroutine(ChargeUp());
    }

    public void Darkened() {
        StartCoroutine(DarkenUp());
    }

    private IEnumerator ChargeUp() {
        for(int i = 0; i < 10; i++)
        {
            chargeSprite.color = new Color(chargeSprite.color.r,chargeSprite.color.g,chargeSprite.color.b,chargeSprite.color.a + 0.1f);
            yield return YieldInstructionCache.WaitingSecond(0.025f);
        }
    }

    private IEnumerator DarkenUp()
    {
        for (int i = 0; i < 10; i++)
        {
            darkenSprite.color = new Color(darkenSprite.color.r, darkenSprite.color.g, darkenSprite.color.b, darkenSprite.color.a + 0.1f);
            yield return YieldInstructionCache.WaitingSecond(0.075f);
        }
    }


    public void ChargeDown() {
        StopCoroutine(ChargeUp());
        StartCoroutine(UnCharge());
;    }

    public void DarkenDown() {
        StopCoroutine(DarkenUp());
        StartCoroutine(UnDarken());
    }

    private IEnumerator UnCharge()
    {
        for (int i = 0; i < 10; i++)
        {
            chargeSprite.color = new Color(chargeSprite.color.r, chargeSprite.color.g, chargeSprite.color.b, chargeSprite.color.a - 0.1f);
            yield return YieldInstructionCache.WaitingSecond(0.025f);
        }
        StopCoroutine(UnCharge());
    }

    private IEnumerator UnDarken() {
        for (int i = 0; i < 10; i++)
        {
            darkenSprite.color = new Color(darkenSprite.color.r, darkenSprite.color.g, darkenSprite.color.b, darkenSprite.color.a - 0.1f);
            yield return YieldInstructionCache.WaitingSecond(0.025f);
        }
        StopCoroutine(UnDarken());
    }

}
