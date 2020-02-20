using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_ddiyong : Character
{

    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;

    [SerializeField]
    private Run abilitySkill;
    // Start is called before the first frame update
    private void Start()
    {
        abilitySkill.Init();
        char_Speed = 7.0f;
        char_Hp = 1000;
        char_Energy = 100;
        char_AbilityPrice = 20;
        char_JumpForce = 5;
        IDInit(1, 2, 2);
        StatInit(char_Speed,char_Hp,char_Energy, char_AbilityPrice, char_JumpForce, abilitySkill);
        RankInit(0,2,3);
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
    }

    public override void UnSpecialAbility()
    {
        base.UnSpecialAbility();
    }

    private void FixedUpdate()
    {
        Move();
    }

}
