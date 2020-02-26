using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBulletExplosion : Enemy
{   

    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Coefficient = 0;
    }

    public void Execute(){
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.5f));
        gameObject.GetComponentInParent<ShotBigBullet>().Reset();
    }

    private void Reset(){
        spriteRenderer.color = Color.white;
        gameObject.transform.localPosition = Vector2.zero;
    }
}
