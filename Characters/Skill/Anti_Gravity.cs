using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Anti_Gravity :MonoBehaviour, ISkill
{
    private Character targetCharacter;
    private Rigidbody2D rBody;
    public void Init()
    {
        targetCharacter = gameObject.GetComponent<Character>();
        rBody = targetCharacter.gameObject.GetComponent<Rigidbody2D>();
    }

    public bool Repeat()
    {
        return true;
    }

    public void Enter()
    {
        StartCoroutine(targetCharacter.ShowEffect(targetCharacter.skilEffect[0]));
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
