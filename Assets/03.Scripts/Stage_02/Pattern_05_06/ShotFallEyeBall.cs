using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFallEyeBall : BossPattern
{

    [SerializeField]
    private int index;
    private FallEyeBall eyeBall;


    public int Index => index;

    private void Awake(){
        eyeBall = gameObject.GetComponentInChildren<FallEyeBall>(true);
    }

    private void Start(){
        Execute();
    }

    public override void Execute(){
        gameObject.SetActive(true);
        NewPositionX(); 
        eyeBall.Execute();
    }


    public void Reset(){
        gameObject.SetActive(false);
    }
}
