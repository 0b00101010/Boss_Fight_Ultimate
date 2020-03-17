using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCtrl : MonoBehaviour, ISkill
{
    private GameObject targetCharacter;

    [SerializeField]
    private Portal[] portals;

    private List<GameObject> createPortal = new List<GameObject>();

    private int index = 0;

    private int count = 2;

    public void Init() {
        targetCharacter = GameObject.FindWithTag("Character");
    }

    public bool Repeat() {
        return false;
    }

    public void Enter()
    {
        createPortal.Add(Instantiate(portals[index].gameObject, targetCharacter.transform.position, Quaternion.identity));
        if (index == 0)
            index = 1;
        else
            index = 0;
    }

    public void Excute() 
    { 
    
    }

    public void Exit()
    {

    }
}
