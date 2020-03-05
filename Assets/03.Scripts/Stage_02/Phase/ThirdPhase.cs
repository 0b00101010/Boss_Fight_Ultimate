using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPhase : Phase
{
    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;

    [SerializeField]
    private Sprite thirdPhaseBackground;

    [SerializeField]
    private BuildingObject buildingObjects;

    [SerializeField]
    private SpriteRenderer blackBackground;

    public override void Execute(){
        backgroundSpriteRenderer.sprite = thirdPhaseBackground;
        buildingObjects.ChangeBulidingColorWhite();
        StartCoroutine(ExecuteCoroutine());
    }
    
    public IEnumerator ExecuteCoroutine(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackBackground,0.1f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(blackBackground,0.1f));
    }
}
