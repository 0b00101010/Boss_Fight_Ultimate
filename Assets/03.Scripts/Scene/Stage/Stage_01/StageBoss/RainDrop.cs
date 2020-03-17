using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : BossPattern
{
    private Vector2 defaultPosition;

    private void Awake(){
        defaultPosition = gameObject.transform.position;
    }

    public override void Execute(){
        NewPositionX();
        gameObject.SetActive(true);
    }

    private void Update(){
        if(gameObject.transform.position.y < -10.0f){
            Reset();    
        }
    }

    private void Reset(){
        gameObject.transform.position = defaultPosition;
        gameObject.SetActive(false);
    }
}
