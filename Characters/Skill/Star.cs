using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour, ISkill{

    private Character targetCharacter;

    public void Init()
    {
        targetCharacter = GameObject.FindWithTag("Character").GetComponent<Character>();
    }

    public bool Repeat()
    {
        return false;
    }

    public void Enter() {
        targetCharacter.transform.tag = "Star";
        
    }

    public void Excute() { 
        
    }

    public void Exit()
    {
        targetCharacter.tag = "Character";

    }

    private IEnumerator Timer()
    {
        var waitingTime = new WaitForSeconds(0.2f);
        SpriteRenderer spriteRenderer = targetCharacter.GetComponent<SpriteRenderer>();
        for(int i = 0; i < 10; i++)
        {
            spriteRenderer.sprite = targetCharacter.skilEffect[1];
            yield return waitingTime;
            spriteRenderer.sprite = targetCharacter.skilEffect[0];
        }
    }
}
