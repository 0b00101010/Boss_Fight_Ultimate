using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EyeBall : Enemy
{

    private SpriteRenderer spriteRenderer;

    private Vector3 smallerSize = Vector3.one / 30f;
    private Rigidbody2D rigidBody;
    private Vector2 direction;

    [SerializeField]
    private EyeBallExplosion explosion;

    private void Awake(){
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Execute(){
        gameObject.SetActive(true);
        rigidBody.velocity = direction * 10;
        StartCoroutine(ExecuteCoroutine());
    }

    public void SetDirection(Vector2 cautionPosition){
        direction = (cautionPosition - (Vector2)gameObject.transform.position).normalized;
    }

    private IEnumerator ExecuteCoroutine(){
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.5f));

        for(int i = 0; i < 15; i++){
            gameObject.transform.localScale -= smallerSize;
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("Boss") && !other.CompareTag("Enemy")){
            explosion.gameObject.transform.position = gameObject.transform.position;
            explosion.Execute();           
            Reset();
        }
    }

    private void Reset(){
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localPosition = Vector2.zero;
        spriteRenderer.color = Color.white;
        rigidBody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}