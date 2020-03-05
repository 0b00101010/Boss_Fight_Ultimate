using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallEyeBallExplosion : MonoBehaviour
{
   private ShotFallEyeBall parentObject;
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        parentObject = gameObject.GetComponentInParent<ShotFallEyeBall>(true);
    }

    private void Start(){
        Execute();
    }

    public void Execute(){
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.25f));
        Reset();
        parentObject.Reset();
    }

    private void Reset(){
        spriteRenderer.color = Color.white;
    }
}
