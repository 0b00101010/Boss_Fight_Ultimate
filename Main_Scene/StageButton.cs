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

    private void Awake()
    {
        if (!Resources.Load("MapData/" + stageName))
            return;
        scoreFile = Resources.Load("MapData/" + stageName) as TextAsset;
        fileText = scoreFile.text;
        fileTexts = fileText.Split(';');
    }

    private void Start()
    {
        //BestScoreValue.UpdateShame(fileTexts[1]);
    }

    public void SetDifficult(string Difficulty) {
        GameManager.instance.Difficulty = Difficulty;
    }

    public void ShowDifficulty() {
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        foreach (GameObject button in difficultyButton)
        {
            bool isShow = button.activeSelf;
            Image image = button.GetComponent<Image>();
            if (!isShow)
            {
                button.gameObject.SetActive(true);
                for (int i = 0; i < 10; i++)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / 10));
                    yield return new WaitForSeconds(0.005f);
                }

            }
            else
            {

                for (int i = 0; i < 10; i++)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / 10));
                    yield return new WaitForSeconds(0.005f);
                }
                button.SetActive(false);

            }
        }

    }


}
