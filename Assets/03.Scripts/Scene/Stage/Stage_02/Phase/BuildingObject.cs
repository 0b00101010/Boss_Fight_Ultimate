using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buildings;
    private List<SpriteRenderer> buildingSpriteRenderers = new List<SpriteRenderer>();

    private IEnumerator moveBuildingCoroutine;
    private Vector2 defaultPosition = Vector2.zero;
    private Vector2 moveVector;

    [SerializeField]
    private Sprite whiteBuildings;

    [SerializeField]
    private Sprite blackBulidings;
    
    [SerializeField]
    private Sprite grayBulidings;

    private void Awake(){
        moveVector = Vector2.left * 0.7f;
        defaultPosition.x = 19.0f;
        defaultPosition.y = 2.0f;
    }

    private void Start(){
        for(int i = 0; i < buildings.Length; i++){
            buildingSpriteRenderers.Add(buildings[i].GetComponent<SpriteRenderer>());
        }
    }

    public void MoveBuilding(){
        moveBuildingCoroutine = MoveBuildingCoroutine();
        StartCoroutine(moveBuildingCoroutine);
    }

    public void StopBuliding(){
        StopCoroutine(moveBuildingCoroutine);
    }

    private IEnumerator MoveBuildingCoroutine(){
        while(true){
            for(int i = 0; i < buildings.Length; i++){
                buildings[i].transform.Translate(moveVector);
                if(buildings[i].gameObject.transform.position.x <= -19.0f){
                    buildings[i].gameObject.transform.position = defaultPosition;
                }
            }    
            yield return YieldInstructionCache.WaitingSecond(0.02f);
        }
    }

    public void ChangeBulidingColorWhite(){
        for(int i = 0; i < buildingSpriteRenderers.Count; i++){
            buildingSpriteRenderers[i].sprite = whiteBuildings;
        }
    }

    public void ChangeBulidingColorGray(){
        for(int i = 0; i < buildingSpriteRenderers.Count; i++){
            buildingSpriteRenderers[i].sprite = grayBulidings;
        }
    }

    public void ChangeBulidingColorBlack(){
        for(int i = 0; i < buildingSpriteRenderers.Count; i++){
            buildingSpriteRenderers[i].sprite = blackBulidings;
        }
    }

    public void BulidingOn(){
        for(int i = 0; i < buildingSpriteRenderers.Count; i++){
            buildingSpriteRenderers[i].gameObject.SetActive(true);
        }
    }

    public void BulidingOff(){
        for(int i = 0; i < buildingSpriteRenderers.Count; i++){
            buildingSpriteRenderers[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator ScaleDownCoroutine(){
        for(int i = 0; i < 60; i++){
            for(int j = 0; j < buildings.Length; j++){
                buildings[j].gameObject.transform.localScale -= (Vector3.one / 200);
            }
            yield return YieldInstructionCache.WaitingSecond(0.05f);
        }
    }

    public void ScaleReset(){
        for(int i = 0; i < buildings.Length; i++){
            buildings[i].gameObject.transform.localScale = Vector3.one;
        }
    }
}
