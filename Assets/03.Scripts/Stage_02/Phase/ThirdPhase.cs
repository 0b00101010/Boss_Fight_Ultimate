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

    public override void Execute(){
        backgroundSpriteRenderer.sprite = thirdPhaseBackground;
        buildingObjects.ChangeBulidingColorWhite();
    }
}
