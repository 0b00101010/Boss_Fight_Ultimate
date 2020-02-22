using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFantasy_Phase_5 : Phase
{
    [SerializeField]
    private SpriteRenderer backGround;

    [SerializeField]
    private Sprite newBackGround;

    [SerializeField]
    private GameObject[] buildings;

    [SerializeField]
    private Sprite newBuilding;
    
    public override void Excute()
    {
        PhaseUP();
    }

    private IEnumerator PhaseUP()
    {
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(backGround, 1.5f));
        backGround.color = new Color(255, 255, 255, 1);

        buildings[0].GetComponent<SpriteRenderer>().enabled = true;
        buildings[1].GetComponent<SpriteRenderer>().enabled = true;
        for (int i = 0; i < buildings.Length; i++)
        {
            buildings[i].GetComponent<SpriteRenderer>().sprite = newBuilding;
        }
    }
}
