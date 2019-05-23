using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Jumper : Character
{
    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;
    private Jumper abilitySkill = new Jumper();
    private void Start()
    {
        abilitySkill.Init();
        char_Hp = 700;
        char_Speed = 7.7f;
        char_Energy = 100;
        char_AbilityPrice = 40;
        char_JumpForce = 5;

        StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, char_JumpForce, abilitySkill);
        RankInit(0, 1, 5);
        //ID 값 변경
        IDInit(6, 26, 6);

    }
}
