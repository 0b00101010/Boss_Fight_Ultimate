using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBugSecondPhase : Phase
{

    [SerializeField]
    private RainDrop[] rain;

    [SerializeField]
    private SpriteRenderer phaseBackground;
    public override void Execute(){
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(phaseBackground,0.5f));
        StartCoroutine(RainDrop());
    }

    private IEnumerator RainDrop(){
        float repeatTime;
        float spendTime = 0;
        repeatTime = Random.Range(0.01f,0.2f);

        while(true){
            if((repeatTime - spendTime) < 0){
                GetUsePossibleRain()?.Execute();
                repeatTime = Random.Range(0.01f,0.1f);
                spendTime = 0;
            }
            else{
                spendTime += Time.deltaTime;
            }
            yield return YieldInstructionCache.WaitFrame;
        }
    }    

    private RainDrop GetUsePossibleRain(){
        for(int i = 0; i < rain.Length; i++){
            if(rain[i].gameObject.activeInHierarchy.Equals(false)){
                return rain[i];
            }
        }
        return null;
    }
}
