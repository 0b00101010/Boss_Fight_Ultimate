using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBigBullet : BossPattern
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BigBullet bigBullet;

    private StageBoss stageBoss;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        stageBoss = GameObject.FindWithTag("Boss").GetComponent<StageBoss>();
    }

    public override void Execute(){
        NewPositionX();
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return YieldInstructionCache.WaitingSecond(0.25f);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.5f));

        bigBullet.gameObject.transform.position = stageBoss.gameObject.transform.position;
        bigBullet.SetDirection(gameObject.transform.position);

        bigBullet.Execute();
    }

    public void Reset(){
        spriteRenderer.color = Color.white;
        gameObject.SetActive(false);
    }
}
