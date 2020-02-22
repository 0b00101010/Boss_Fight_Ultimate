using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageBlock : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public IEnumerator FadeOut(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 1.5f));
        gameObject.SetActive(false);
    }
}
