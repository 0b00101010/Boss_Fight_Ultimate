using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<IObserver> Observers = new List<IObserver>();
    
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
    public AchievementManager achievementManager;

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

        achievementManager = gameObject.GetComponent<AchievementManager>();
        soundManager = gameObject.GetComponent<SoundManager>();
        touchManager = gameObject.GetComponent<TouchManager>();
        AddObserver(new AchievementObserver());
        touchManager.SetOnSwipeDectected(myOnSwipeDectected);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        achievementManager.LoadAchievement();
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[0]);
        GameManager.instance.soundManager.MusicQueue();
    }

    private void Update()
    {
        touchManager.ProcessMobileInput();
    }

    public float GetScore() {
        return LastGameScore;
    }

    private void myOnSwipeDectected(Vector2 SwipeDirection)
    {
        Debug.DrawLine((Vector2)transform.position,(Vector2)transform.position + SwipeDirection,Color.red,4.0f);
    }

    public IEnumerator FadeIn(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 10) {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a + (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator FadeOut(SpriteRenderer spriteRenderer, float spendTime, int repeatCount = 10) {
        for (int i = 0; i < repeatCount; i++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeIn(Image image, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public IEnumerator IFadeOut(Image image, float spendTime, int repeatCount = 10)
    {
        for (int i = 0; i < repeatCount; i++)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (1.0f / repeatCount));
            yield return new WaitForSeconds(spendTime / repeatCount);
        }
    }

    public void AddObserver(IObserver observer)
    {
        Observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        Observers.Remove(observer);
    }

    public void Notify(int eventTag)
    {
        foreach (IObserver observer in Observers)
        {
            observer.Renewal(eventTag);
        }
    }
}