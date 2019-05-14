using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleStage : MonoBehaviour
{
    private Character gameChar;

    private int beat;
    [SerializeField]
    private int lastBeat;
    [SerializeField]
    private float beatUpSpeed;
    [SerializeField]
    private ShameCtrl BeatShame = null;
    [SerializeField]
    private ShameCtrl HpShame = null;
    [SerializeField]
    private ShameCtrl EnergyShame = null;

    public int Beat { get => beat; set => beat = value; }

    private void Awake()
    {
        gameChar = GameObject.FindWithTag("Character").GetComponent<Character>();
    }

    private void Start()
    {
        StartCoroutine(BeatUp());
        StartCoroutine(StageUpdate());
    }

    private IEnumerator BeatUp()
    {
        Beat++;
        BeatShame.UpdateShame(Beat);
        yield return new WaitForSeconds(beatUpSpeed);
        if (Beat < lastBeat)
        {
            StartCoroutine(BeatUp());
        }
        else if (Beat == lastBeat)
        {
            GameManager.instance.Notify((int)GameManager.ObserveTag.GAME_CLEAR);
        }


    }

    private IEnumerator StageUpdate()
    {
        HpShame.UpdateShame((int)gameChar.Hp);
        EnergyShame.UpdateShame(gameChar.Energy);
       
        if (gameChar.Hp < 0)
        {
            GameManager.instance.Notify((int)GameManager.ObserveTag.CHARACTER_DEATH);
            //StopCoroutine(BeatUp());
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(StageUpdate());
        }
    }

    public void MoveLeft()
    {
        gameChar.SetLeft();
    }

    public void MoveRight()
    {
        gameChar.SetRight();
    }

    public void Jump()
    {
        gameChar.Jump();
    }

    public void SpecialAbility()
    {
        gameChar.SpecialAbility();
    }

    public void UnAbility()
    {
        gameChar.UnSpecialAbility();
    }
}
