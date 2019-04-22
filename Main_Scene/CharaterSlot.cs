using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterSlot : MonoBehaviour
{
    [SerializeField]
    private bool unLock;

    public bool UnLock { get => unLock; set => unLock = value; }

    [SerializeField]
    private GameObject character;

    private void CharacterUnLock()
    {
        UnLock = true;
    }

    public GameObject GetCharacter() {
        return character;
    }
}
