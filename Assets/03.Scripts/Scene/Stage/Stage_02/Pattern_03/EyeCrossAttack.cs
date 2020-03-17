using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeCrossAttack : BossPattern
{   

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;

    [SerializeField]
    private Sprite blackSprite;

    [SerializeField]
    private Sprite whiteSprite;

    [SerializeField]
    private Sprite backgroundBlackSprite;

    [SerializeField]
    private Sprite backgroundWhiteSprite;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void Execute(){
        NewPositionX();
        NewPositionY();
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        gameObject.tag = "Enemy";
        
        backgroundSpriteRenderer.sprite = backgroundBlackSprite;
        spriteRenderer.sprite = whiteSprite;
        // yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.01f));
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(backgroundSpriteRenderer,0.1f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(spriteRenderer,0.1f));
        // yield return YieldInstructionCache.WaitingSecond(0.1f);
        backgroundSpriteRenderer.sprite = backgroundWhiteSprite;
        spriteRenderer.sprite = blackSprite;

        gameObject.tag = "Untagged";
    }
}
