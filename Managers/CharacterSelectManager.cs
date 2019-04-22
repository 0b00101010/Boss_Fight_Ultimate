using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField]
    private List<CharaterSlot> charactersSlot = new List<CharaterSlot>();
    [SerializeField]
    private List<GameObject> characters = new List<GameObject>();
    private CharaterSlot selectSlot;
    [SerializeField]
    private Sprite[] slotSprites;

    private IObserver observer;

    //0 기본 1 선택 2 잠김
    private void Start()
    {
        foreach (CharaterSlot slot in charactersSlot)
        {
            if (slot.UnLock)
                slot.GetComponent<SpriteRenderer>().sprite = slotSprites[0];
            else
                slot.GetComponent<SpriteRenderer>().sprite = slotSprites[2];

            if (slot.GetCharacter().Equals(GameManager.instance.nowGameCharacter))
            {
                selectSlot = slot;
                selectSlot.GetComponent<SpriteRenderer>().sprite = slotSprites[1];
            }
        }
       //charactersSlot[characters.IndexOf(GameManager.instance.nowGameCharacter)].GetComponent<SpriteRenderer>().sprite = slotSprites[1];
    }

    private void Update() { 
   
        if (GameManager.instance.touchManager.IsSwiped)
            MoveCharacterSlots();

        if (GameManager.instance.touchManager.IsTouch)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(GameManager.instance.touchManager.GetPosition());
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0.0f);

            if(hit.collider.gameObject.CompareTag("Slot"))
            {
                Debug.Log(hit.collider.name);
                selectSlot = hit.collider.gameObject.GetComponent<CharaterSlot>();
            }

        }
    }

    private void MoveCharacterSlots() {
        if (GameManager.instance.touchManager.SwipeDirection.x < 0 && charactersSlot[charactersSlot.Count].transform.position.x > 6.0f)
        {
            foreach (CharaterSlot characterSlot in charactersSlot)
            {
                characterSlot.gameObject.transform.Translate(new Vector2(-2.0f, 0.0f));
            }
        }
        else if (GameManager.instance.touchManager.SwipeDirection.x > 0 && charactersSlot[0].transform.position.x < -6.0f){
            foreach (CharaterSlot characterSlot in charactersSlot) {
                characterSlot.gameObject.transform.Translate(new Vector2(2.0f, 0.0f));
            }
        }
    }

    private void SelectCharacter() {
        GameManager.instance.nowGameCharacter = selectSlot.GetCharacter();
    }


}
