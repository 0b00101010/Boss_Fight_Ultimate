using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISkill{

    private GameObject targetObject;

    public void Init()
    {
        targetObject = GameObject.FindWithTag("Character");
    }

    public void Enter() {
        targetObject.tag = "Star";
    }

    public void Excute() { 
        
    }

    public void Exit()
    {
        targetObject.tag = "Character";
    }

}
