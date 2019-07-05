using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : Enemy
{

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Destroy(gameObject,3.0f);
        Coefficient = 1.5f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Excute());
    }

    private IEnumerator Excute()
    {
        for(int i = 0; i < 5; i++)
        {
            gameObject.transform.Translate(new Vector2(0.0f,0.5f));
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.transform.tag = "Ground";
        yield return new WaitForSeconds(1.85f);
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer,0.25f));
    }
}
