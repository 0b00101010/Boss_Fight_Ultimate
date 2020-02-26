using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildGrowth : BossPattern
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprout[] sprouts;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Execute(){
        NewPositionX();
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return YieldInstructionCache.WaitingSecond(0.25f);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.5f));
        
        spriteRenderer.enabled = false;
        
        yield return StartCoroutine(sprouts[Random.Range(0, sprouts.Length)].Execute());

        gameObject.SetActive(false);
        Reset();        
    }

    private void Reset(){
        spriteRenderer.enabled = true;
        spriteRenderer.color = Color.white;
    }
}
