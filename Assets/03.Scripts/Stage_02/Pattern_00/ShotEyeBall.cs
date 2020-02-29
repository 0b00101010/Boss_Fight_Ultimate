using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEyeBall : BossPattern
{
    private StageBoss stageBoss;
    private EyeBall eyeBall;

    private void Awake(){
        stageBoss = GameObject.FindWithTag("Boss").GetComponent<StageBoss>();
        eyeBall = gameObject.GetComponentInChildren<EyeBall>(true);
    }

    private void Start(){
        Execute();
    }

    public override void Execute(){
        gameObject.SetActive(true);
        NewPositionX();
        eyeBall.gameObject.transform.position = stageBoss.gameObject.transform.position;
        eyeBall.SetDirection(gameObject.transform.position);    
        eyeBall.Execute();
    }


    public void Reset(){
        gameObject.SetActive(false);
    }
}
