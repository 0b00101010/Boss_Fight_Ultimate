using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : ISkill
{
    private Character targetCharacter;
    private float plusSpeed;

    public void Init() {
        targetCharacter = GameObject.FindWithTag("Character").GetComponent<Character>();
    }

    public void Enter()
    {
        plusSpeed = (targetCharacter.Speed / 40) * 100;
        targetCharacter.Speed += plusSpeed;
    }
    
    public void Excute()
    {

    }

    public void Exit() {
        targetCharacter.Speed -= plusSpeed;
    }

}

