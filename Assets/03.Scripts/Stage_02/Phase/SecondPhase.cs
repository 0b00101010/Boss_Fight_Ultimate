using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SecondPhase : Phase
{
    [SerializeField]
    private SpriteRenderer backgroundSpriteRenderer;

    [SerializeField]
    private Sprite secondPhaseBackground;

    [SerializeField]
    private SpriteRenderer whiteBackground;

    [SerializeField]
    private SpriteRenderer blackBackground;

    [SerializeField]
    private BuildingObject buildingObjects;
    
    [SerializeField]
    private GroundController groundController;

    public override void Execute(){
        backgroundSpriteRenderer.sprite = secondPhaseBackground;
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        whiteBackground.color = Color.white;
        blackBackground.color = Color.white;
        
        buildingObjects.MoveBuilding();
        groundController.PanelGroundOn();

        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(whiteBackground,0.2f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(blackBackground,0.2f));
        whiteBackground.color = Color.white;
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(whiteBackground,0.2f));

    
    }
    
    
}
