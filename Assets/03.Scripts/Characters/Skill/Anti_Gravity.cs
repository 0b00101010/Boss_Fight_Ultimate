using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Anti_Gravity :MonoBehaviour, ISkill
{
    private Character targetCharacter;
    private Rigidbody2D rBody;

    private bool IsAbility = false;
        
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
        IsAbility = true;
    }

    public void Excute() {
    }

    public void Exit()
    {
        IsAbility = false;
        rBody.gravityScale = 1.0f;
        targetCharacter.JumpForce *= -1;
    }

    private void FixedUpdate()
    {
        if (!IsAbility)
            return;

        if (gameObject.transform.position.y + 0.5f < 5.0f)
            gameObject.transform.Translate(new Vector3(0, 0.5f, 0));
        else if (gameObject.transform.position.y + 0.5f > 5.0f)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 4.5f, gameObject.transform.position.z);


    }
}
