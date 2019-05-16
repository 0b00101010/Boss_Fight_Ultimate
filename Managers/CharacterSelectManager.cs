using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterSlot> charactersSlot = new List<CharacterSlot>();
    [SerializeField]
    private List<GameObject> characters = new List<GameObject>();
    private CharacterSlot selectSlot;
    [SerializeField]
    private Sprite[] slotSprites;

    private IObserver observer;

    // 0 잠김 1 기본 2 선택 
    // 3 기본 4 선택 레어 
    // 5 기본 6 선택 유니크
    private void Start()
    {
        foreach (CharacterSlot slot in charactersSlot)
        {
            if (slot.UnLock)
            {
                SpriteRenderer slotSpriteRenderer = slot.GetComponent<SpriteRenderer>();
                Character slotCharacter = slot.GetCharacter().GetComponent<Character>();
                switch (slotCharacter.Rank) {
                    case 0:
                        slotSpriteRenderer.sprite = slotSprites[1];
                        slot.SpriteNumber = 1;
                        break;
                    case 1:
                        slotSpriteRenderer.sprite = slotSprites[3];
                        slot.SpriteNumber = 3;
                        break;
                    case 2:
                        slotSpriteRenderer.sprite = slotSprites[5];
                        slot.SpriteNumber = 5;
                        break;
                }
            }
            else
            {
                slot.GetComponent<SpriteRenderer>().sprite = slotSprites[0];
            }
        }
        //charactersSlot[characters.IndexOf(GameManager.instance.nowGameCharacter)].GetComponent<SpriteRenderer>().sprite = slotSprites[1];
        foreach (CharacterSlot slot in charactersSlot)
        {
            if (slot.GetCharacter().Equals(GameManager.instance.nowGameCharacter))
            {
                slot.GetComponent<SpriteRenderer>().sprite = slotSprites[slot.SpriteNumber += 1];
                selectSlot = slot;
            }
        }
    }

    private void Update() { 
   
        if (GameManager.instance.touchManager.IsSwiped)
            MoveCharacterSlots();

        if (GameManager.instance.touchManager.IsTouch)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(GameManager.instance.touchManager.GetPosition());
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0.0f);

            if(hit.collider.gameObject.CompareTag("Slot") && hit.collider.gameObject.GetComponent<CharacterSlot>().UnLock)
            {
                Debug.Log(hit.collider.name);
                selectSlot.GetComponent<SpriteRenderer>().sprite = slotSprites[selectSlot.SpriteNumber -= 1];
                selectSlot = hit.collider.gameObject.GetComponent<CharacterSlot>();
                selectSlot.GetComponent<SpriteRenderer>().sprite = slotSprites[selectSlot.SpriteNumber += 1];

                SelectCharacter();
            }

        }
    }

    private void MoveCharacterSlots() {
        if (GameManager.instance.touchManager.SwipeDirection.x < 0 && charactersSlot[charactersSlot.Count].transform.position.x > 6.0f)
        {
            foreach (CharacterSlot characterSlot in charactersSlot)
            {
                characterSlot.gameObject.transform.Translate(new Vector2(-2.0f, 0.0f));
            }
        }
        else if (GameManager.instance.touchManager.SwipeDirection.x > 0 && charactersSlot[0].transform.position.x < -6.0f){
            foreach (CharacterSlot characterSlot in charactersSlot) {
                characterSlot.gameObject.transform.Translate(new Vector2(2.0f, 0.0f));
            }
        }
    }

    private void SelectCharacter() {
        GameManager.instance.nowGameCharacter = selectSlot.GetCharacter();
    }


}
