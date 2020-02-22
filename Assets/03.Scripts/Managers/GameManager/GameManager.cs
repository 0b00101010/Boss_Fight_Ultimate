using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region ENUM
    public enum AchievementsTag { LADYBUG_SCORE_3000, LIFE_IS_BEAUTIFULE };
    public enum ObserveTag { GAME_CLEAR, GAME_END , CHARACTER_DEATH};
    public enum StageTag { LADYBUG = 3};
    #endregion

    private float lastGameScore;
    private float lastGameHp;
    private int lastGameHitCount;
    private int nextStageNumber;
    private int money;

    public AudioClip[] GameMusics;

    public int NextStageNumber { get => nextStageNumber; set => nextStageNumber = value; }
    public int Money { get => money; set => money = value; }
    public float LastGameScore { get => lastGameScore; set => lastGameScore = value; }
    public float LastGameHp { get => lastGameHp; set => lastGameHp = value; }
    public int LastGameHitCount { get => lastGameHitCount; set => lastGameHitCount = value; }

    public TouchManager touchManager;
    public SoundManager soundManager;
    public FadeManager fadeManager;


    public Sprite[] LoadImages;
    public Sprite[] ResultImages;
    public GameObject nowGameCharacter;
    public StageButton nowPlayStage;


    public string[] FilePaths;
    public string[] StageNames;
    public string Difficulty;

    private void Awake() {
        if (instance == null)
            instance = this;

        soundManager = gameObject.GetComponent<SoundManager>();
        touchManager = gameObject.GetComponent<TouchManager>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[0]);
        GameManager.instance.soundManager.MusicQueue();
    }

    private void Update()
    {
        touchManager.ProcessTouch();
    }

    public float GetScore() {
        return LastGameScore;
    }

 
}