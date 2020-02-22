using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : Enemy
{
    [SerializeField]
    private float Damage;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rBody;
    private void Start()
    {
        Coefficient = Damage;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rBody = gameObject.GetComponent<Rigidbody2D>();

        rBody.velocity = new Vector2(Random.Range(-20, 20f), Random.Range(-20f, 20f));

        StartCoroutine(Excute());
        Destroy(gameObject,1.2f);
    }

    private IEnumerator Excute()
    {
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,1.0f));
        
    }
}

