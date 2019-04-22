using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Passives : MonoBehaviour
{

    public enum PassiveNumber {QUIKMOVEMENT = 0, STRONG, RECHARGE, ROLLBACK, TRIPPLEJUMP, RESTORE, ACCELERATION, PASS, OVERCHARGE};
    //기민함||민첩함 , 강인함, 재충전, 역행, 삼단점프, 복구, 가속
    private Character targetCharacter = GameManager.instance.nowGameCharacter.GetComponent<Character>() ;

    public void QuickMoveMent(){  }

    public void Strong() { }

    public void Recharge() { }

    public void RollBack() { }

    public void TrippleJump() { }

    public void ReStore() { }

    public void Acceleration() { }

}
