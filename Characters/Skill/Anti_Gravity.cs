using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anti_Gravity : ISkill
{
    private Character targetCharacter;
    private Rigidbody2D rBody;
    public void Init()
    {
        Debug.Log("Anti_Init");
        targetCharacter = GameObject.FindWithTag("Character").GetComponent<Character>();
        rBody = targetCharacter.gameObject.GetComponent<Rigidbody2D>();
    }

    public bool Repeat()
    {
        return true;
    }

    public void Enter()
    {
        rBody.gravityScale = 0.0f;
        targetCharacter.JumpForce *= -1;
    }

    public void Excute() {

    }

    public void Exit()
    {
        rBody.gravityScale = 1.0f;
        targetCharacter.JumpForce *= -1;
    }
}
