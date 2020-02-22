using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Explode : Enemy
{
    private SpriteRenderer spriteRenderer; 

    // Start is called before the first frame update
    private void Start()
    {
        Coefficient = 0.5f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Excute());
    }

    private IEnumerator Excute()
    {
        yield return YieldInstructionCache.WaitingSecond(0.25f);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.5f));
        Destroy(gameObject);
    }

}
