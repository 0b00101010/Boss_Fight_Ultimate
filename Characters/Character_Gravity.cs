﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Gravity : Character
{
    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;
    private SpriteRenderer spriteRenderer;
    private Anti_Gravity ability = new Anti_Gravity();

    public Sprite[] sprites; // 능력에 따라 스프라이트 변경
    // Start is called before the first frame update
    private void Start()
    {
        ability.Init();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        char_Speed = 6.3f;
        char_Hp = 1000;
        char_Energy = 100;
        char_AbilityPrice = 20;
        char_JumpForce = 5;
        rBody = gameObject.GetComponent<Rigidbody2D>();
        IDInit(2,2,2);
        StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, char_JumpForce);
        RankInit(0,2,3);
    }

    public override void SpecialAbility()
    {
        ability.Enter();
        base.SpecialAbility();
        // 능력사용 스프라이트 변경
    }

    public override void UnSpecialAbility()
    {
        ability.Exit();
        base.UnSpecialAbility();
        // 능력 비활성화 스프라이트 변경
    }

    private void FixedUpdate()
    {
        Move();
        if (IsUseAbility)
        {
            if (gameObject.transform.position.y < 4.5f)
                gameObject.transform.Translate(new Vector3(0, 0.5f, 0));
        }

        if (Energy - AbilityPrice <= 0)
            UnSpecialAbility();
    }

}
