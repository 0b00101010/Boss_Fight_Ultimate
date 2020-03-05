using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buildings;
    private IEnumerator moveBuildingCoroutine;
    private Vector2 defaultPosition = Vector2.zero;
    private Vector2 moveVector;

    [SerializeField]
    private Sprite whiteBuildings;
    
    [SerializeField]
    private Sprite blackBulidings;

    private void Awake(){
        moveVector = Vector2.left * 0.7f;
        defaultPosition.x = 19.0f;
        defaultPosition.y = 2.0f;
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

    
}
