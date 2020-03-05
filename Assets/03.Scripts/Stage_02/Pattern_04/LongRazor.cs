using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRazor : BossPattern
{
    public override void Execute(){
        gameObject.SetActive(true);
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            gameObject.transform.Translate(Vector2.down);
            yield return YieldInstructionCache.WaitingSecond(0.05f);

            if(gameObject.transform.position.y < -35){
               break; 
            }
        }

        Reset();
    }

    private void Reset(){
        gameObject.SetActive(false);
    }

}
