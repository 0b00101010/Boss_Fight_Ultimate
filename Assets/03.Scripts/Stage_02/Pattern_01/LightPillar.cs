using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : BossPattern{

    [SerializeField]
    private int index; // 0 : column 1: rows
    
    private ChaosFantasyLight chaosFantasyLight;
    
    private SpriteRenderer spriteRenderer;

    public int Index => index;

    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        chaosFantasyLight = gameObject.GetComponentInChildren<ChaosFantasyLight>(true);
    }
    
    public override void Execute(){
        if(index.Equals(0)){
            NewPositionX();
        }
        else{
            NewPositionY();
        }
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.1f));
        yield return StartCoroutine(chaosFantasyLight.Execute());
        Reset();
    }

    private void Reset(){
        spriteRenderer.color = Color.white;
        gameObject.SetActive(false);
    }
}
