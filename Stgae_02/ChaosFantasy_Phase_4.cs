using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFantasy_Phase_4 : Phase
{
    [SerializeField]
    private GameObject[] buildings;

    [SerializeField]
    private Boss_ChaosFantasy stageBoss;

    [SerializeField]
    private SpriteRenderer backGround;

    [SerializeField]
    private Sprite newBackGround;

    public override void Excute()
    {
        buildings[0].GetComponent<SpriteRenderer>().enabled = false;
        buildings[1].GetComponent<SpriteRenderer>().enabled = false;
        backGround.sprite = newBackGround;
        stageBoss.GetComponent<EyeCrossAttack>().EndCrossAttack();
        stageBoss.Vibrate();
    }
}
