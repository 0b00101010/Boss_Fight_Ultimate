using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour, ISkill
{
    private GameObject targetObject;

    public void Init() {
        targetObject = GameObject.FindWithTag("Character");
    }

    public bool Repeat() {
        return false;
    }


    public void Enter() {
        Vector2 force = new Vector2(0, 10);
        Rigidbody2D rBody = targetObject.GetComponent<Rigidbody2D>();
        rBody.velocity = force;
    }

    public void Excute() {

    }

    public void Exit() { 
    
    }

}
