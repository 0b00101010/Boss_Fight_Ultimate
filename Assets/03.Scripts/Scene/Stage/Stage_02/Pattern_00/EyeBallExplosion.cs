using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallExplosion : MonoBehaviour
{
    private ShotEyeBall parentObject;
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        parentObject = gameObject.GetComponentInParent<ShotEyeBall>(true);
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
        gameObject.SetActive(false);
        spriteRenderer.color = Color.white;
    }
    
}
