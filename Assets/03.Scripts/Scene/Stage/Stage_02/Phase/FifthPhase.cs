using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthPhase : Phase
{

    [SerializeField]
    private BuildingObject buildingObject;
    
    [SerializeField]
    private GroundController groundController;

    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;

    [SerializeField]
    private SpriteRenderer whiteBackgroundSpriteRenderer;

    [SerializeField]
    private Sprite fifthPhaseBackgroundWhite;

    [SerializeField]
    private Sprite fifthPhaseBackgroundBlack;

    public override void Execute(){
        StartCoroutine(ExeucteCoroutine());
    }

    private IEnumerator ExeucteCoroutine(){
        groundController.PanelGroundOff();
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(whiteBackgroundSpriteRenderer,2.5f));
        backgroundSpriteRenderer.sprite = fifthPhaseBackgroundWhite;
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(whiteBackgroundSpriteRenderer,2.5f));

        buildingObject.ChangeBulidingColorGray();
        buildingObject.BulidingOn();

        yield return StartCoroutine(buildingObject.ScaleDownCoroutine());
        buildingObject.ScaleReset();
        buildingObject.ChangeBulidingColorBlack();

        backgroundSpriteRenderer.sprite = fifthPhaseBackgroundBlack;
    }
}