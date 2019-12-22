using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageManager : MonoBehaviour
{
    private int beat;
    private int score;
    [SerializeField]
    private int lastBeat = 0;

    [SerializeField]
    private float beatUpSpeed = 0;

    [SerializeField]
    private List<int> phaseUpBeat = new List<int>();
    private AudioSource audioSource;

    public int Beat { get => beat; set => beat = value; }


    [SerializeField]
    private GameObject blackBackground = null;
     
    [SerializeField]
    private Phase[] phases;

    [SerializeField]
    private ShameCtrl BeatShame = null;
    [SerializeField]
    private ShameCtrl HpShame = null;
    [SerializeField]
    private ShameCtrl EnergyShame = null;

    private Character _char;

    private int phaseUpCount = 0;
    private string filePath;
    private ICharacter gameChar;

    [SerializeField]
    private PatternFileRead patterRead;

    [SerializeField]
    private int musicNumber = 0;
    [SerializeField]
    private int filePathNumber = 0;

    private void Awake()
    {
        CreateCharacter();
        audioSource = GameManager.instance.GetComponent<AudioSource>();
        filePath = GameManager.instance.FilePaths[filePathNumber] + "_" + GameManager.instance.Difficulty;
    }

    private void Start()
    {
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[musicNumber]);
        GameManager.instance.soundManager.MusicQueue();
        StartCoroutine(BeatUp());
        StartCoroutine(StageUpdate());
        HpShame.UpdateShame((int)_char.Hp);
        EnergyShame.UpdateShame(_char.Energy);
        patterRead.ReadFile(filePath);
    }



    private IEnumerator BeatUp()
    {
        Beat++;
        BeatShame.UpdateShame(Beat);
        yield return new WaitForSeconds(beatUpSpeed);
        if (beat < lastBeat)
        {
            StartCoroutine(BeatUp());
        }
        else if (beat == lastBeat)
        {
            StartCoroutine(GameEnd());
        }

        if (phases.Length > phaseUpCount)
        {
            if(beat.Equals(phaseUpBeat[phaseUpCount]))
                PhaseUp();
        }

    }

    private void CreateCharacter()
    {
        _char = Instantiate(GameManager.instance.nowGameCharacter,new Vector2(0f,0f),Quaternion.identity).GetComponent<Character>();
        _char.tag = "Character";
        gameChar = _char.GetComponent<ICharacter>();
    }


    private void PhaseUp()
    {
       
        phases[phaseUpCount].Excute();
        phaseUpCount++;
    }

    private IEnumerator BlackIn()
    {

        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(GameManager.instance.FadeIn(blackspriteRenderer, 1.0f));
        for (int i = 0; i < 20; i++)
        {
            audioSource.spatialBlend += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        audioSource.Stop();
        audioSource.spatialBlend = 0.5f;
    }

    private IEnumerator GameEnd()
    {
        GameManager.instance.LastGameScore = (_char.Hp / _char.MaxHp) * 100;
        if (GameManager.instance.LastGameScore < 0)
            GameManager.instance.LastGameScore = 0;
       
         GameManager.instance.LastGameHp = _char.Hp;
        if (GameManager.instance.LastGameHp < 0)
            GameManager.instance.LastGameHp = 0;

        yield return StartCoroutine(BlackIn());
        SceneManager.LoadScene("03_GameResult");
    }

    private IEnumerator StageUpdate()
    {
        HpShame.UpdateShame((int)_char.Hp);
        EnergyShame.UpdateShame(_char.Energy);
        patterRead.CreatePattern(Beat);
        if (_char.Hp < 0)
        {
            //StopCoroutine(BeatUp());
            _char.Death();
            StartCoroutine(GameEnd());
        }
        else
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(StageUpdate());
        }
    }

#if UNITY_EDITOR_OSX
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            gameChar.SetLeft();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            gameChar.SetLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            gameChar.SetRight();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            gameChar.SetRight();
        if (Input.GetKeyDown(KeyCode.Z))
            gameChar.Jump();
        if (Input.GetKeyDown(KeyCode.X))
            gameChar.SpecialAbility();
        if (Input.GetKeyUp(KeyCode.X))
            gameChar.UnSpecialAbility();


    }
#endif

    public void MoveLeft()
    {
        gameChar.SetLeft();
    }

    public void MoveRight()
    {
        gameChar.SetRight();
    }

    public void Jump()
    {
        gameChar.Jump();
    }

    public void SpecialAbility()
    {
        gameChar.SpecialAbility();
    }

    public void UnAbility()
    {
        gameChar.UnSpecialAbility();
    }

}
