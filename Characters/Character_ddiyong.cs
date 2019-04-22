using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_ddiyong : Character
{

    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private float PlusSpeed;
    private int char_AbilityPrice;
    private int char_JumpForce;
    // Start is called before the first frame update
    private void Start()
    {
        char_Speed = 7.0f;
        char_Hp = 1000;
        char_Energy = 100;
        PlusSpeed =(char_Speed / 40) * 100;
        char_AbilityPrice = 20;
        char_JumpForce = 5;
        Init(char_Speed,char_Hp,char_Energy, char_AbilityPrice, char_JumpForce);  
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        base.Speed += PlusSpeed;
    }

    public override void UnSpecialAbility()
    {
        base.UnSpecialAbility();
        base.Speed -= PlusSpeed;

    }

    private void FixedUpdate()
    {
        Move();
    }

}
