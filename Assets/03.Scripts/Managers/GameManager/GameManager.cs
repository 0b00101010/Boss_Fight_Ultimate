using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public enum AchievementsTag { LADYBUG_SCORE_3000, LIFE_IS_BEAUTIFULE };
    public enum ObserveTag { GAME_CLEAR, GAME_END , CHARACTER_DEATH};
    public enum StageTag { LADYBUG = 3};

    public AudioClip[] GameMusics;

    public TouchManager touchManager;
    public SoundManager soundManager;
    public FadeManager fadeManager;

    public Sprite[] LoadImages;
    public Sprite[] ResultImages;
    public GameObject nowGameCharacter;
    public StageButton nowPlayStage;

    public string[] FilePaths;
    public string[] StageNames;

    private void Awake() {
        if (instance == null)
            instance = this;

        soundManager = gameObject.GetComponent<SoundManager>();
        touchManager = gameObject.GetComponent<TouchManager>();
        fadeManager = gameObject.GetComponent<FadeManager>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start(){
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[0]);
        GameManager.instance.soundManager.MusicQueue();
    }

    private void Update(){
        touchManager.ProcessTouch();
    }
}