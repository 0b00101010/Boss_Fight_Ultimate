using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultSceneManager : MonoBehaviour
{
    private float score;
    private TextAsset BestScoreFile;

    [SerializeField]
    private Image backGround;

    public Sprite[] ranks;
    public Sprite[] survivedResult;



    private void Awake()
    {
        score = GameManager.instance.LastGameScore;

        if (!(System.IO.File.Exists(Application.dataPath + "/Resources/MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4] + "_" + GameManager.instance.Difficulty + ".txt")))
        {
            //System.IO.File.Create(Application.dataPath + "/Resources/MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4 ] + "_" +GameManager.instance.Difficulty + ".txt");
            CreateNewScoreFile();
        }
        GameManager.instance.GetComponent<AudioSource>().Play();
        BestScoreFile = Resources.Load("MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber-4] + "_" + GameManager.instance.Difficulty) as TextAsset;
        Debug.Log("/MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4] + "_" + GameManager.instance.Difficulty);
        string  str = BestScoreFile.text;
        string[] BestScoreText = str.Split(';');



        if (score > int.Parse(BestScoreText[1]))
            CreateNewScoreFile();
    }
    private void Start()
    {
        backGround.sprite = GameManager.instance.ResultImages[GameManager.instance.NextStageNumber - 4];

    }
    private void CreateNewScoreFile() {
        System.IO.File.WriteAllText(Application.dataPath + 
        "/Resources/Mapdata/"+GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4] + "_" + GameManager.instance.Difficulty + ".txt","BestScore;"+score);
    }

    public void ReturnMainScene()
    {
        GameManager.instance.Notify((int)GameManager.ObserveTag.GAME_END);
        GameManager.instance.soundManager.MusicStop();
        GameManager.instance.GetComponent<AudioSource>().spatialBlend = 0.0f;
        SceneManager.LoadScene(1);
    }

    public void Retry() {
        GameManager.instance.soundManager.MusicStop();
        SceneManager.LoadScene("02_LoadScene");
    }
}
