using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFantasy_Phase_3 : Phase
{

    [SerializeField]
    private SpriteRenderer blackBackground;

    [SerializeField]
    private SpriteRenderer background;

    [SerializeField]
    private Sprite newBackGround;

    [SerializeField]
    private GameObject[] buildings;

    [SerializeField]
    private Sprite newBuilding;

    [SerializeField]
    private SpriteRenderer Eye;

    [SerializeField]
    private Boss_ChaosFantasy stageBoss;


    public override void Excute()
    {
        StartCoroutine(PhaseUp());
    }

    private IEnumerator PhaseUp()
    {
        stageBoss.StopBlink();
        StartCoroutine(GameManager.instance.FadeOut(Eye, 0.35f));
        yield return StartCoroutine(GameManager.instance.FadeIn(blackBackground, 0.35f));
        for(int i = 0; i < buildings.Length; i++)
        {
            buildings[i].GetComponent<SpriteRenderer>().sprite = newBuilding;
        }

        yield return new WaitForSeconds(0.5f);

        background.sprite = newBackGround;
        yield return StartCoroutine(GameManager.instance.FadeOut(blackBackground, 0.35f));
        stageBoss.GetComponentInChildren<EyeCrossAttack>().CrossAttackInit();

    }
}
