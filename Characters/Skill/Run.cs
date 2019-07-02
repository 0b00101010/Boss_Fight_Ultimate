using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour, ISkill
{
    private Character targetCharacter;
    private float plusSpeed;
    private float basicSpeed;

    public void Init() {
        targetCharacter = gameObject.GetComponent<Character>();
    }

    public bool Repeat()
    {
        return true;
    }

    public void Enter()
    {
        basicSpeed = targetCharacter.Speed;
        plusSpeed = (targetCharacter.Speed / 40) * 100;
        targetCharacter.Speed += plusSpeed;
    }
    
    public void Excute(){
      
    }

    public void Exit() {
        targetCharacter.Speed = basicSpeed;
    }

}

