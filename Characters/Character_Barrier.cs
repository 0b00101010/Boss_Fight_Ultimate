using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Barrier : Character
{
    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;
    // Start is called before the first frame update
    private void Start()
    {
        char_Speed = 6.3f;
        char_Hp = 1400;
        char_Energy = 100;
        char_AbilityPrice = 20;
        char_JumpForce = 5;
        IDInit(3,3,3);
        StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, char_JumpForce);
        RankInit(0, 4, 1);
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
    }

    public override void UnSpecialAbility()
    {
        base.UnSpecialAbility();
    }
}
