using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterSlot> charactersSlot = new List<CharacterSlot>();
    [SerializeField]
    private List<GameObject> characters = new List<GameObject>();
    private CharacterSlot selectSlot;
    [SerializeField]
    private Sprite[] slotSprites;
    [SerializeField]
    private SpriteRenderer blackBackGround;
    private CharacterUICtrl uiCtrl;
    private IObserver observer;

    public static CharacterSelectManager instance;
    // 0 잠김 1 기본 2 선택 
    // 3 기본 4 선택 레어 
    // 5 기본 6 선택 유니크
    private void Start()
    {
        if (instance == null)
            instance = this;

        selectSlot = charactersSlot[0];
        uiCtrl = new CharacterUICtrl();
        uiCtrl.Init();
        StartCoroutine(GameManager.instance.FadeOut(blackBackGround,0.5f));

        foreach (CharacterSlot slot in charactersSlot)
        {
            if (slot.UnLock)
            {
                Image slotImage = slot.GetComponent<Image>();
                Character slotCharacter = slot.GetCharacter().GetComponent<Character>();
                switch (slotCharacter.Rank) {
                    case 0:
                        slotImage.sprite = slotSprites[1];
                        slot.SpriteNumber = 1;
                        break;
                    case 1:
                        slotImage.sprite = slotSprites[3];
                        slot.SpriteNumber = 3;
                        break;
                    case 2:
                        slotImage.sprite = slotSprites[5];
                        slot.SpriteNumber = 5;
                        break;
                }
            }
            else
            {
                slot.GetComponent<Image>().sprite = slotSprites[0];
            }
        }
        //charactersSlot[characters.IndexOf(GameManager.instance.nowGameCharacter)].GetComponent<Image>().sprite = slotSprites[1];
        foreach (CharacterSlot slot in charactersSlot)
        {
            if (slot.GetCharacter().Equals(GameManager.instance.nowGameCharacter))
            {
                slot.GetComponent<Image>().sprite = slotSprites[slot.SpriteNumber += 1];
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
                selectSlot.GetComponent<Image>().sprite = slotSprites[selectSlot.SpriteNumber -= 1];
                selectSlot = hit.collider.gameObject.GetComponent<CharacterSlot>();
                selectSlot.GetComponent<Image>().sprite = slotSprites[selectSlot.SpriteNumber += 1];

                SelectCharacter();
            }

        }
    }

    private void MoveCharacterSlots() {
        if (GameManager.instance.touchManager.SwipeDirection.x < 0 && charactersSlot[charactersSlot.Count].GetComponent<RectTransform>().position.x > 120.0f)
        {
            foreach (CharacterSlot characterSlot in charactersSlot)
            {
                characterSlot.gameObject.GetComponent<RectTransform>().Translate(new Vector2(-80.0f,0.0f));
            }
        }
        else if (GameManager.instance.touchManager.SwipeDirection.x > 0 && charactersSlot[0].transform.position.x < 680.0f){
            foreach (CharacterSlot characterSlot in charactersSlot) {
                characterSlot.gameObject.GetComponent<RectTransform>().Translate(new Vector2(+80.0f, 0.0f));
            }
        }
    }

    public void MainSceneLoad()
    {
        StartCoroutine(ReturnMainScene());
    }
    
    private IEnumerator ReturnMainScene()
    {
        yield return StartCoroutine(GameManager.instance.FadeIn(blackBackGround,0.5f));
        SceneManager.LoadScene(2);
    }

    private void SelectCharacter() {
        GameManager.instance.nowGameCharacter = selectSlot.GetCharacter();
    }

    public CharacterSlot GetSelectSlot()
    {
        return selectSlot;
    }
}
