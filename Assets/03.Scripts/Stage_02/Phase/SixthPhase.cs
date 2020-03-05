using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthPhase : Phase
{   
    [SerializeField]
    private BuildingObject buildingObject;

    [SerializeField]
    private SpriteRenderer blackBackground;

    public override void Execute(){
        buildingObject.BulidingOff();
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackBackground,0.5f));
    } 
}
