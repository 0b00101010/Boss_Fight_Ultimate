using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISkill{

    private GameObject targetObject;

    public void Init()
    {
        targetObject = GameObject.FindWithTag("Character");
    }

    public bool Repeat()
    {
        return false;
    }

    public void Enter() {
        targetObject.tag = "Star";
        StartCoroutine(Timer());
    }

    public void Excute() { 
        
    }

    public void Exit()
    {

    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(2.0f);
        targetObject.tag = "Character";
    }
}
