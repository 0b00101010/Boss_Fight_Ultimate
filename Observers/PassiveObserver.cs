using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObserver : MonoBehaviour, IObserver
{
    public void Renewal(int eventTag)
    {
        switch (eventTag)
        {
            case (int)Character_Passives.PassiveNumber.QUIKMOVEMENT:
                break;
            case (int)Character_Passives.PassiveNumber.STRONG:
                break;
            case (int)Character_Passives.PassiveNumber.RECHARGE:
                break;
            case (int)Character_Passives.PassiveNumber.ROLLBACK:
                break;
            case (int)Character_Passives.PassiveNumber.RESTORE:
                break;
            case (int)Character_Passives.PassiveNumber.ACCELERATION:
                break;
            case (int)Character_Passives.PassiveNumber.PASS:
                break;
            case (int)Character_Passives.PassiveNumber.OVERCHARGE:
                break;
        }
    }
}
