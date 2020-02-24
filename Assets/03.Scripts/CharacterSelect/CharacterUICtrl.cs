using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUICtrl : MonoBehaviour
{
    #region UI

    [SerializeField]
    private Image characterImage;
    [SerializeField]
    private Text characterNameText;
    [SerializeField]
    private Text characterSkillText;
    [SerializeField]
    private Text characterAbilityText;
    [SerializeField]
    private Text characterDescripts;
    [SerializeField]
    private Text characterRank;

    [SerializeField]
    private Image[] tankLevels;

    [SerializeField]
    private Image[] dodgeLevels;

    [SerializeField]
    private Sprite[] levelImage;

    #endregion UI

    private CharacterSlot selectSlot;

    // TODO : Change to use scriptableobject file
    private List<string> nameList = new List<string>();
    private List<string> descriptList = new List<string>();
    private List<string> skilList = new List<string>();
    private List<string> abilityList = new List<string>();
    
    public void Awake()
    {   TextAsset names = Resources.Load("Character/CharactersName") as TextAsset;
        TextAsset descript = Resources.Load("Character/CharactersDescripts") as TextAsset;
        TextAsset skil = Resources.Load("Character/CharactersSkil") as TextAsset;
        TextAsset ability = Resources.Load("Character/CharactersAbility") as TextAsset;

        string st = names.text;
        string[] strs = st.Split('\n');

        foreach (string str in strs)
            nameList.Add(str);

        st = descript.text;
        strs = st.Split('\n');
        foreach (string str in strs)
            descriptList.Add(str);

        st = skil.text;
        strs = st.Split('\n');

        foreach (string str in strs)
            skilList.Add(str);

        st = ability.text;
        strs = st.Split('\n');

        foreach (string str in strs)
            abilityList.Add(str);
    }

    public IEnumerator InformationUpdate()
    {
        selectSlot = CharacterSelectManager.instance.GetSelectSlot();
        GameObject target = Instantiate(selectSlot.GetCharacter(), Vector2.one * -100, Quaternion.identity);
        characterImage.sprite = target.GetComponent<SpriteRenderer>().sprite;

        int index = CharacterSelectManager.instance.GetSelectSlotNumber();
        characterNameText.text = nameList[index];
        characterDescripts.text = descriptList[index];
        characterSkillText.text = skilList[index];
        characterAbilityText.text = abilityList[index];

        yield return YieldInstructionCache.WaitingSecond(0.01f);

        for (int i = 0; i < tankLevels.Length; i++)
            tankLevels[i].sprite = levelImage[0];

        for (int i = 0; i < dodgeLevels.Length; i++)
            dodgeLevels[i].sprite = levelImage[0];


        for (int i = 0; i < target.GetComponent<Character>().Level_Tank; i++)
            tankLevels[i].sprite = levelImage[1];

        for (int i = 0; i < target.GetComponent<Character>().Level_Dodge; i++)
            dodgeLevels[i].sprite = levelImage[1];

        switch (selectSlot.GetCharacter().GetComponent<Character>().Rank)
        {
            case 0:
                characterRank.text = "Normal";
                break;
            case 1:
                characterRank.text = "Rare";
                break;
            case 2:
                characterRank.text = "Unique";
                break;
        }
        Destroy(target);
        
    }

}
