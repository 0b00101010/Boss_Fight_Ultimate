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

    #endregion UI

    private CharacterSlot selectSlot;

    public void Init()
    {
        selectSlot = CharacterSelectManager.instance.GetSelectSlot();
        InfomationUpdate();
    }

    public void InfomationUpdate()
    {
        characterImage.sprite = selectSlot.GetCharacter().GetComponent<Character>().GetSprite();
        //characterNameText
        //characterSkillText
        //characterAbilityText
        //characterDescripts
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
    }
}
