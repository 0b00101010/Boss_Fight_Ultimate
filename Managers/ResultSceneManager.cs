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
        GameManager.instance.soundManager.MusicQueue();
        score = GameManager.instance.LastGameScore;
        backGround.sprite = GameManager.instance.ResultImages[GameManager.instance.NextStageNumber - 4];
        if (!(System.IO.File.Exists(Application.dataPath + "/Resources/MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4] + "_" + GameManager.instance.Difficulty + ".txt")))
        {
            //System.IO.File.Create(Application.dataPath + "/Resources/MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4 ] + "_" +GameManager.instance.Difficulty + ".txt");
            CreateNewScoreFile();
        }
        BestScoreFile = Resources.Load("MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber-4] + "_" + GameManager.instance.Difficulty) as TextAsset;
        Debug.Log("/MapData/" + GameManager.instance.StageNames[GameManager.instance.NextStageNumber - 4] + "_" + GameManager.instance.Difficulty);
        string  str = BestScoreFile.text;
        string[] BestScoreText = str.Split(';');

        Debug.Log(BestScoreText[1]);

        if (score > float.Parse(BestScoreText[1]))
            CreateNewScoreFile();
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
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[0]);
        GameManager.instance.soundManager.MusicQueue();
        SceneManager.LoadScene(1);

    }

    public void Retry() {
        GameManager.instance.soundManager.MusicStop();
        SceneManager.LoadScene("02_LoadScene");
    }
}
