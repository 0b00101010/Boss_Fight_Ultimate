using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageButton : MonoBehaviour
{

    private int bestScore = 0;
    private TextAsset scoreFile;

    [SerializeField]
    private string stageName;
    [SerializeField]
    private GameObject[] difficultyButton;
    private string fileText;
    private string[] fileTexts;
    private ShameCtrl BestScoreValue;
    private bool isButtonClick = false;

    private void Awake()
    {
        if (!Resources.Load("MapData/" + stageName))
            return;
        scoreFile = Resources.Load("MapData/" + stageName) as TextAsset;
        fileText = scoreFile.text;
        fileTexts = fileText.Split(';');
    }

    public void SetDifficult(string difficulty) {
        PlayerPrefs.SetString("Difficulty", difficulty);
    }

    public void ShowDifficulty() {
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut(){
        if(isButtonClick){
            yield break;
        }
        isButtonClick = true;
        foreach (GameObject button in difficultyButton){
            bool isShow = button.activeSelf;
            Image image = button.GetComponent<Image>();
            if (!isShow){
                button.gameObject.SetActive(true);
                for (int i = 0; i < 10; i++){
                    image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / 10));
                    yield return YieldInstructionCache.WaitFrame;
                }
            }
            else{
                for (int i = 0; i < 10; i++){
                    image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / 10));
                    yield return YieldInstructionCache.WaitFrame;
                }
                button.SetActive(false);
            }
        }
        isButtonClick = false;
    }
}
