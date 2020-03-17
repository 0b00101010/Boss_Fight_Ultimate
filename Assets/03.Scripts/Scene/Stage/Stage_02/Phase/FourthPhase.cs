using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthPhase : Phase
{

    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;

    [SerializeField]
    private Sprite fourthPhaseBackground;
    
    [SerializeField]
    private BuildingObject buildingObject;

    [SerializeField]
    private SpriteRenderer backgroundObjectSpriteRenderer;
    
    [SerializeField]
    private SpriteRenderer backgroundBlackSpriteRenderer;
    
    private Vector3 rotateVector = Vector3.zero;

    public override void Execute(){
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        buildingObject.StopBuliding();
        buildingObject.BulidingOff();

        backgroundSpriteRenderer.sprite = fourthPhaseBackground;

        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(backgroundBlackSpriteRenderer,0.5f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(backgroundBlackSpriteRenderer,0.5f));

        for(int i = 0; i < 5; i++){
            yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(backgroundObjectSpriteRenderer,0.3f));
            yield return YieldInstructionCache.WaitingSecond(0.5f);
        
            rotateVector.z = Random.Range(0,180);
            backgroundObjectSpriteRenderer.gameObject.transform.Rotate(rotateVector);
        }
    }
}
