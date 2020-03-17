using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SeedBullet : Enemy
{

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float damage;

    private Rigidbody2D rigidBody;
    private Vector2 flyingForce;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        Coefficient = damage;
    }
    
    public IEnumerator Execute(){
        gameObject.SetActive(true);
        
        flyingForce.x = Random.Range(-15.0f, 15.0f);
        flyingForce.y = Random.Range(-15.0f, 15.0f);
        
        rigidBody.velocity = flyingForce;

        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,1.0f));
        
        Reset();
    }
    
    private void Reset(){
        spriteRenderer.color = Color.white;
        rigidBody.velocity = Vector2.zero;
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
}
