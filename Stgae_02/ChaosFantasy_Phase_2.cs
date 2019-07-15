using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFantasy_Phase_2 : Phase
{
    [SerializeField]
    private SpriteRenderer whiteBackGround;

    [SerializeField]
    private SpriteRenderer blackBackGround;

    public override void Excute()
    {
        StartCoroutine(BlinkPhaseUp());
    }


    private IEnumerator BlinkPhaseUp()
    {
        whiteBackGround.color = new Color(255, 255, 255, 1);
        blackBackGround.color = new Color(255, 255, 255, 1);
        yield return StartCoroutine(GameManager.instance.FadeOut(whiteBackGround, 0.25f));
        whiteBackGround.color = new Color(255, 255, 255, 1);
        yield return StartCoroutine(GameManager.instance.FadeOut(blackBackGround,0.3f));
        yield return StartCoroutine(GameManager.instance.FadeIn(blackBackGround, 0.3f));
        whiteBackGround.color = new Color(255, 255, 255, 0);
        yield return StartCoroutine(GameManager.instance.FadeOut(blackBackGround, 0.15f));


    }
}
