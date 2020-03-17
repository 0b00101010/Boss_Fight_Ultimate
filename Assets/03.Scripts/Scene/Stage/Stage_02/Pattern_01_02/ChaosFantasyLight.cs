using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFantasyLight : Enemy
{
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Coefficient = 0.3f;
    }

    public IEnumerator Execute(){
        gameObject.SetActive(true);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.25f));
        Reset();
    }
    
    private void Reset(){
        spriteRenderer.color = Color.white;
        gameObject.SetActive(false);
    }
}
