using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterSelectManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterSlot> charactersSlot = new List<CharacterSlot>();

    private CharacterSlot selectSlot;
    [SerializeField]
    private Sprite[] slotSprites;
    [SerializeField]
    private SpriteRenderer blackBackGround;
    private CharacterUICtrl uiCtrl;
    private IObserver observer;

    

    public static CharacterSelectManager instance;

    public CharacterSlot SelectSlot
    {
        get => selectSlot;

        set
        {
            selectSlot = value;
            StartCoroutine(uiCtrl.InformationUpdate());
        }

    }

    private void Awake()
    {
        TextAsset slotsUnlocked = Resources.Load("Character/Characters") as TextAsset;
        string text = slotsUnlocked.text;
        string[] strs = text.Split('\n');

        for(int i = 0; i < charactersSlot.Count; i++)
        {
            string[] isUnlock = strs[i].Split(';');
            if (isUnlock[1].Equals("1"))
                charactersSlot[i].UnLock = true;
            else
                charactersSlot[i].UnLock = false;
        }
            

        uiCtrl = gameObject.GetComponent<CharacterUICtrl>();

    }

    // 0 잠김 1 기본 2 선택 
    // 3 기본 4 선택 레어 
    // 5 기본 6 선택 유니크
    private void Start()
    {
        if (instance == null)
            instance = this;


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
                SelectSlot = slot;
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
                SelectSlot.GetComponent<Image>().sprite = slotSprites[SelectSlot.SpriteNumber -= 1];
                SelectSlot = hit.collider.gameObject.GetComponent<CharacterSlot>();
                SelectSlot.GetComponent<Image>().sprite = slotSprites[SelectSlot.SpriteNumber += 1];

                SelectCharacter();
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
            SelectSlot = charactersSlot[0];

        if (Input.GetKeyDown(KeyCode.B))
            SelectSlot = charactersSlot[1];

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
        SceneManager.LoadScene("01_Stage_Select");
    }

    private void SelectCharacter() {
        GameManager.instance.nowGameCharacter = SelectSlot.GetCharacter();
    }

    public CharacterSlot GetSelectSlot()
    {
        return SelectSlot;
    }

    public int GetSelectSlotNumber()
    {
		return charactersSlot.IndexOf(selectSlot);
    }

}
