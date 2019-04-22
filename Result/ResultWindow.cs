using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultWindow : MonoBehaviour
{

    #region UIS
    [SerializeField]
    private SpriteRenderer resultSquare;
    [SerializeField]
    private SpriteRenderer survivedResult;
    [SerializeField]
    private SpriteRenderer emphasis;
    [SerializeField]
    private SpriteRenderer rank;
    [SerializeField]
    private SpriteRenderer remainedHp;
    [SerializeField]
    private SpriteRenderer hit;
    [SerializeField]
    private Image returnToMain;
    [SerializeField]
    private Image reTry;
    [SerializeField]
    private ShameCtrl remainedValue;
    [SerializeField]
    private ShameCtrl hitValue;
    #endregion UIS

    [SerializeField]
    private ResultSceneManager sceneManager;
    private void Start()
    {
        StartCoroutine(ResultWindowCtrl());
    }
    private IEnumerator SquareGlow()
    {
        resultSquare.transform.localScale = new Vector2(resultSquare.transform.localScale.x + 5, resultSquare.transform.localScale.y);
        yield return new WaitForSeconds(0.005f);
        if (resultSquare.transform.localScale.x < 95)
            StartCoroutine(SquareGlow());
        else
            StopCoroutine(SquareGlow());
    }

    private IEnumerator ResultWindowCtrl()
    {
        yield return StartCoroutine(SquareGlow());
        if (GameManager.instance.LastGameScore > 0)
            survivedResult.sprite = sceneManager.survivedResult[0];
        else
            survivedResult.sprite = sceneManager.survivedResult[1];

        StartCoroutine(GameManager.instance.FadeIn(emphasis, 0.3f));
        StartCoroutine(GameManager.instance.FadeIn(hit, 0.3f));
        StartCoroutine(GameManager.instance.FadeIn(survivedResult, 0.3f));
        StartCoroutine(GameManager.instance.FadeIn(remainedHp, 0.3f));

        remainedValue.UpdateShame((int)GameManager.instance.LastGameHp);
        hitValue.UpdateShame(GameManager.instance.LastGameHitCount);
        rank.sprite = sceneManager.ranks[CompareRank()];

        for(int i =0; i < 10; i++)
        {
            rank.color = new Color(rank.color.r, rank.color.g, rank.color.b, rank.color.a + 0.05f);
            rank.transform.localScale = new Vector2(rank.transform.localScale.x - 20, rank.transform.localScale.y - 20);
            yield return new WaitForSeconds(0.005f);
        }

        for (int i = 0; i < 10; i++)
        {
            returnToMain.color = new Color(returnToMain.color.r, returnToMain.color.g, returnToMain.color.b, returnToMain.color.a + (1.0f / 10));
            reTry.color = new Color(reTry.color.r, returnToMain.color.g, reTry.color.b, reTry.color.a + (1.0f / 10));
            yield return new WaitForSeconds(0.02f);
        }
    }

    private int CompareRank() {
        int score = (int)GameManager.instance.LastGameScore;
        if (score == 100)
            return 0;
        else if (score > 80)
            return 1;
        else if (score > 60)
            return 2;
        else if (score > 40)
            return 3;
        else if (score > 20)
            return 4;
        else
            return 5;
    }

}
