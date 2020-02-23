using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    [SerializeField]
    private bool unLock;

    private int spriteNumber;

    public bool UnLock { get => unLock; set => unLock = value; }
    public int SpriteNumber { get => spriteNumber; set => spriteNumber = value; }
   
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
