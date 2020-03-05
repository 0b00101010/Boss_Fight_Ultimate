using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallEyeBall : Enemy
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;

    [SerializeField]
    private FallEyeBallExplosion explosion;

    private void Awake(){
        rigidBody = gameObject.GetComponent<Rigidbody2D>();    
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Execute(){
        gameObject.SetActive(true);
        rigidBody.velocity = Vector2.down * 2;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("Boss") && !other.CompareTag("Enemy")){
            explosion.gameObject.transform.position = gameObject.transform.position;
            explosion.Execute();           
            Reset();
        }
    }

    private void Reset(){
        rigidBody.velocity = Vector2.zero;
        gameObject.transform.localPosition = Vector2.zero;
        gameObject.SetActive(false);
    }
}
