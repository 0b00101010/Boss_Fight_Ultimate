using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Gravity : Character
{
    private float char_Speed;
    private int char_Hp;
    private int char_Energy;
    private int char_AbilityPrice;
    private int char_JumpForce;

    [SerializeField]
    private Anti_Gravity abilitySkill;

    // Start is called before the first frame update
    private void Start()
    {
        abilitySkill.Init();
        char_Speed = 6.3f;
        char_Hp = 1000;
        char_Energy = 100;
        char_AbilityPrice = 20;
        char_JumpForce = 5;
        rBody = gameObject.GetComponent<Rigidbody2D>();
        IDInit(2,2,2);
        StatInit(char_Speed, char_Hp, char_Energy, char_AbilityPrice, char_JumpForce,abilitySkill);
        RankInit(0,2,3);
    }

    public override void SpecialAbility()
    {
        base.SpecialAbility();
        // 능력사용 스프라이트 변경
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
