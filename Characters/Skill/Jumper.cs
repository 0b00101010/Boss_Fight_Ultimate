using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour, ISkill
{
    private Character targetCharacter;

    public void Init() {
        targetCharacter = gameObject.GetComponent<Character>() ;
    }

    public bool Repeat() {
        return false;
    }


    public void Enter() {
        Vector2 force = new Vector2(0, 10);
        Rigidbody2D rBody = targetCharacter.GetComponent<Rigidbody2D>();
        rBody.velocity = force;
        StartCoroutine(Effect());
    }

    public void Excute() {

    }

    public void Exit() { 
    
    }

    private IEnumerator Effect()
    {
        var waitingTime = new WaitForSeconds(0.1f);
        for(int i = 0; i < 5; i++)
        {
            targetCharacter.ShowEffect(targetCharacter.skilEffect[1]);
            yield return waitingTime;
        }
    }
}
