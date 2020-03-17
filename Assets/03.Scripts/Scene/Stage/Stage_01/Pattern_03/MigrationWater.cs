using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MigrationWater : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Execute(){
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        RandomSetting();
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,1.0f));
        Reset();
    }

    private void RandomSetting(){
        Vector2 newScale = Vector2.zero;
        
        float randomScale = Random.Range(1.5f, 2.0f);
        
        newScale.x = randomScale;
        newScale.y = randomScale;

        gameObject.transform.localScale = newScale;

        Color newColor = Color.white;
        
        float randomAlpha = Random.Range(0.5f, 1.0f);

        newColor.a = randomAlpha;

        spriteRenderer.color = newColor; 
    }

    private void Reset(){
        gameObject.SetActive(false);
        spriteRenderer.color = Color.white;
    }
}
