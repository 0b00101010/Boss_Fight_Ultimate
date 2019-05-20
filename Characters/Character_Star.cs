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

    private Star ability = new Star();

    private void Start()
    {
        ability.Init();
        char_Speed = 7.0f;
        char_Hp = 1000;
        char_Energy = 100;
        char_AbilityPrice = 100;
        char_JumpForce = 5;

        StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, JumpForce);
        IDInit(4, 4, 4);
        RankInit(0, 4, 1);
    }

    public override void SpecialAbility()
    {
        ability.Enter();
        StartCoroutine(Timer());
        base.SpecialAbility();
    }

    private IEnumerator Timer() {
        yield return new WaitForSeconds(2.0f);
        ability.Exit();
    }

}
