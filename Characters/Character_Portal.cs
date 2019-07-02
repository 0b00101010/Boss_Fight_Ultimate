using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Portal : Character
{

    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;

    [SerializeField]
    private PortalCtrl abilitySkill;
    private void Start()
    {
        char_Hp = 800;
        char_Speed = 7f;
        char_Energy = 100;
        char_AbilityPrice = 50;
        char_JumpForce = 5;
        //StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, char_JumpForce);
        IDInit(7, 7, 7);
        RankInit(0, 1, 5);
    }
}
