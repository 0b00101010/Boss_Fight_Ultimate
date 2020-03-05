using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaosFantasy : StageBoss, StagePattern
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private List<BossPattern> eyeBalls = new List<BossPattern>();
    private List<BossPattern> lightPillarColumns = new List<BossPattern>();
    private List<BossPattern> lightPillarRows = new List<BossPattern>();
    private BossPattern eyeCrossAttack;
    private BossPattern longRazor;

    private List<BossPattern> firstFallEyeBalls = new List<BossPattern>();
    private List<BossPattern> secondFallEyeBalls = new List<BossPattern>();


    [SerializeField]
    private Phase[] phases;
    private int currentPhase;

    [SerializeField]
    private GameObject defaultBoss;

    private ViberateEye viberateEye;

    [SerializeField]
    private GameObject patternObject; 

    private void Awake(){
        BossPattern[] tempPattern;
        
        tempPattern = patternObject.GetComponentsInChildren<ShotEyeBall>(true);
        
        for(int i = 0; i < tempPattern.Length; i++){
            eyeBalls.Add(tempPattern[i]);
        }

        LightPillar[] pillars = patternObject.GetComponentsInChildren<LightPillar>(true);

        for(int i = 0; i < pillars.Length; i++){
            if(pillars[i].Index.Equals(0)){
                lightPillarColumns.Add(pillars[i]);
            }
            else{
                lightPillarRows.Add(pillars[i]);
            }
        }

        ShotFallEyeBall[] fallEyeBalls = patternObject.GetComponentsInChildren<ShotFallEyeBall>(true);

        for(int i = 0; i < fallEyeBalls.Length; i++){
            if(fallEyeBalls[i].Index.Equals(0)){
                firstFallEyeBalls.Add(fallEyeBalls[i]);
            }
            else{
                secondFallEyeBalls.Add(fallEyeBalls[i]);
            }
        }

        longRazor = patternObject.GetComponentInChildren<LongRazor>(true);
        
        eyeCrossAttack = gameObject.GetComponentInChildren<EyeCrossAttack>(true);
        viberateEye = gameObject.GetComponentInChildren<ViberateEye>(true);
    }

    public void Execute(int patternNumber){
        switch(patternNumber){
            case 0:
            StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.1f));
            GetUsePossiblePattern(eyeBalls).Execute();
            StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(spriteRenderer, 0.1f));
            break;

            case 1:
            GetUsePossiblePattern(lightPillarColumns).Execute();
            break;

            case 2:
            GetUsePossiblePattern(lightPillarRows).Execute();
            break;

            case 3:
            eyeCrossAttack.Execute();
            break;

            case 4:
            longRazor.Execute();
            break;

            case 5:
            GetUsePossiblePattern(firstFallEyeBalls).Execute();
            break;

            case 6:
            GetUsePossiblePattern(secondFallEyeBalls).Execute();
            break;
        }
    }

    public void PhaseUp(){
        phases[currentPhase].Execute();
        StateChange(currentPhase++);
    }

    public void StateChange(int stateNumber){
        switch(stateNumber){
            case 0:
            break;

            case 1:
            defaultBoss.gameObject.SetActive(false);
            StartCoroutine(StateEyeCrossAttack());
            break;

            case 2:
            eyeCrossAttack.gameObject.SetActive(false);
            StartCoroutine(StateVibrateEye());
            break;

            case 5:
            viberateEye.gameObject.SetActive(false);
            break;
        }
    }

    private IEnumerator StateEyeCrossAttack(){
        yield return YieldInstructionCache.WaitingSecond(0.2f);
        eyeCrossAttack.gameObject.SetActive(true);
    }

    private IEnumerator StateVibrateEye(){
        yield return YieldInstructionCache.WaitingSecond(1.0f);
        viberateEye.gameObject.SetActive(true);
        viberateEye.Execute();
    }

    public BossPattern GetUsePossiblePattern(List<BossPattern> bossPatterns){
        for(int i = 0; i < bossPatterns.Count; i++){
            if(bossPatterns[i].gameObject.activeInHierarchy.Equals(false)){
                return bossPatterns[i];
            }
        }
        
        return null;
    }
}
