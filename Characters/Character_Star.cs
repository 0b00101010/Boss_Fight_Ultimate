using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Star : Character
{

    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;

    private Star abilitySkill = new Star();

    private void Start()
    {
        abilitySkill.Init();
        char_Speed = 7.0f;
        char_Hp = 1000;
        char_Energy = 100;
        char_AbilityPrice = 100;
        char_JumpForce = 5;

        StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, char_JumpForce, abilitySkill);
        IDInit(4, 4, 4);
        RankInit(0, 4, 1);
    }

    private void FixedUpdate() {
        Move();
    }
}
