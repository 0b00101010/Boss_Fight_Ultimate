using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : Enemy
{
    private SpriteRenderer spriteRenderer;
    private Vector2 growSpeed;
    private Vector2 defaultPosition;


    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        growSpeed = Vector2.zero;
        growSpeed.y = 0.5f;
        Coefficient = 1.5f;
        defaultPosition = gameObject.transform.localPosition;
    }
    
    public IEnumerator Execute(){
        gameObject.SetActive(true);

        for(int i = 0; i < 5; i++){
            gameObject.transform.Translate(growSpeed);
            yield return YieldInstructionCache.WaitingSecond(0.1f);
        }

        gameObject.transform.tag = "Ground";

        yield return YieldInstructionCache.WaitingSecond(1.85f);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.25f));
        
        Reset();
    }   

    private void Reset(){
        spriteRenderer.color = Color.white;
        gameObject.transform.localPosition = defaultPosition;
        gameObject.SetActive(false);
    }
}
