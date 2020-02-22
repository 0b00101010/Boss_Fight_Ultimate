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



    private void Awake(){
        GameManager.instance.soundManager.MusicQueue();
        score = PlayerPrefs.GetFloat("LastGameScore");
        backGround.sprite = GameManager.instance.ResultImages[PlayerPrefs.GetInt("NextStage") - 5];
        if (!(System.IO.File.Exists(Application.dataPath + "/Resources/MapData/" + GameManager.instance.StageNames[PlayerPrefs.GetInt("NextStage") - 5] + "_" + PlayerPrefs.GetString("Difficulty") + ".txt"))){
            CreateNewScoreFile();

        }
        BestScoreFile = Resources.Load("MapData/" + GameManager.instance.StageNames[PlayerPrefs.GetInt("NextStage") - 5] + "_" + PlayerPrefs.GetString("Difficulty")) as TextAsset;
        string  str = BestScoreFile.text;
        string[] BestScoreText = str.Split(';');

        if (score > float.Parse(BestScoreText[1])){
            CreateNewScoreFile();
        }
    }

    private void CreateNewScoreFile() {
        System.IO.File.WriteAllText(Application.dataPath + 
        "/Resources/Mapdata/"+GameManager.instance.StageNames[PlayerPrefs.GetInt("NextStage") - 5] + "_" + PlayerPrefs.GetString("Difficulty") + ".txt","BestScore;"+score);
    }

    public void ReturnMainScene()
    {
        GameManager.instance.soundManager.MusicStop();
        GameManager.instance.GetComponent<AudioSource>().spatialBlend = 0.0f;
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[0]);
        GameManager.instance.soundManager.MusicQueue();
        SceneManager.LoadScene("01_Stage_Select");
    }

    public void Retry() {
        GameManager.instance.soundManager.MusicStop();
        SceneManager.LoadScene("02_LoadScene");
    }
}
