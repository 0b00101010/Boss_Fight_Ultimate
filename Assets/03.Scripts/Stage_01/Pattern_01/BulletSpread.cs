using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpread : MonoBehaviour, IBossPattern
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private SpriteRenderer crosshairSpriteRenderer;
    private Vector3 crosshairSmallerSize;

    [SerializeField]
    private SeedBullet[] bullets;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        crosshairSmallerSize = new Vector3(0.1f,0.1f,0.1f);

    }
    public void Execute(){
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    public IEnumerator ExecuteCoroutine(){
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.5f));
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(crosshairSpriteRenderer,0.5f));

        for(int i = 0; i < 5; i++){
            crosshairSpriteRenderer.gameObject.transform.localScale -= crosshairSmallerSize;
            yield return YieldInstructionCache.WaitingSecond(0.1f);
        }

        spriteRenderer.enabled = false;
        crosshairSpriteRenderer.enabled = false;

        for(int i = 0; i < bullets.Length - 1; i++){
            StartCoroutine(bullets[i].Execute());
            yield return YieldInstructionCache.WaitFrame;
        }

        yield return StartCoroutine(bullets[bullets.Length-1].Execute());

        Reset();
    }

    private void Reset(){
        spriteRenderer.enabled = true;
        crosshairSpriteRenderer.enabled = true;

        spriteRenderer.color = Color.white;
        crosshairSpriteRenderer.color = Color.white;

        crosshairSpriteRenderer.gameObject.transform.localScale = Vector3.one;

        gameObject.SetActive(false);
    }
}
