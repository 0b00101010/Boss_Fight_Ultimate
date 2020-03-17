using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBarrier : MonoBehaviour, ISkill
{
    private Character targetCharacter;
    private int curEnergy;

    private GameObject barrier;

    public CreateBarrier(GameObject barrier)
    {
        this.barrier = barrier;
    }

    public void Init() {
        targetCharacter = GameObject.FindWithTag("Character").GetComponent<Character>();
    }

    public bool Repeat()
    {
        return false;
    }

    public void Enter(){
        curEnergy = targetCharacter.Energy;
        GameObject instance = Instantiate(barrier,targetCharacter.transform.position,Quaternion.identity);
        instance.GetComponent<Barrier>().SetBarrierValue(curEnergy);
        instance.transform.SetParent(targetCharacter.transform);
        targetCharacter.ShowEffect(targetCharacter.skilEffect[0]);
    }

    public void Excute(){
       
    }

    public void Exit(){

    }

}
