using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletExplosion : Enemy
{   
    private Color defaultColor;
    private SpriteRenderer spriteRenderer;
    private ShotBigBullet parentObject;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        parentObject = gameObject.GetComponentInParent<ShotBigBullet>();
        
        defaultColor = spriteRenderer.color;
        Coefficient = 0;
    }

    public void Execute(){
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.5f));
        parentObject.Reset();
        Reset();
    }

    private void Reset(){
        spriteRenderer.color = defaultColor;
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
}
