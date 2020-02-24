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
    private ValueCtrl BeatShame = null;
    [SerializeField]
    private ValueCtrl HpShame = null;
    [SerializeField]
    private ValueCtrl EnergyShame = null;

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

    private void Awake(){
        CreateCharacter();
        audioSource = GameManager.instance.GetComponent<AudioSource>();
        filePath = GameManager.instance.FilePaths[filePathNumber] + "_" + PlayerPrefs.GetString("Difficulty");
    }

    private void Start(){
        GameManager.instance.soundManager.MusicChange(GameManager.instance.GameMusics[musicNumber]);
        GameManager.instance.soundManager.MusicQueue();
        StartCoroutine(BeatUp());
        StartCoroutine(StageUpdate());
        HpShame.UpdateValue((int)_char.Hp);
        EnergyShame.UpdateValue(_char.Energy);
        patterRead.ReadFile(filePath);
    }



    private IEnumerator BeatUp(){
        Beat++;
        BeatShame.UpdateValue(Beat);

        yield return new WaitForSeconds(beatUpSpeed);
        
        if (beat < lastBeat){
            StartCoroutine(BeatUp());
        }
        else if (beat == lastBeat){
            StartCoroutine(GameEnd());
        }

        if (phases.Length > phaseUpCount){
            if(beat.Equals(phaseUpBeat[phaseUpCount]))
                PhaseUp();
        }

    }

    private void CreateCharacter(){
        GameObject gameCharacter = Resources.Load<GameObject>("Characters/" + PlayerPrefs.GetString("SelectCharacter"));
        _char = Instantiate(gameCharacter, Vector2.zero,Quaternion.identity).GetComponent<Character>();
        _char.gameObject.transform.tag = "Character";
        gameChar = _char.GetComponent<ICharacter>();
    }

    private void PhaseUp(){
        phases[phaseUpCount].Excute();
        phaseUpCount++;
    }

    private IEnumerator BlackIn(){
        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackspriteRenderer, 1.0f));
        for (int i = 0; i < 20; i++){
            audioSource.spatialBlend += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        audioSource.Stop();
        audioSource.spatialBlend = 0.5f;
    }

    private IEnumerator GameEnd()
    {
        PlayerPrefs.SetFloat("LastGameScore", (_char.Hp / _char.MaxHp) * 100);
        if (PlayerPrefs.GetFloat("LastGameScore") <  0){
            PlayerPrefs.SetFloat("LastGameScore", 0);
        }

        PlayerPrefs.SetFloat("LastGameHp", _char.Hp);
        
        if (PlayerPrefs.GetFloat("LastGameScore") < 0){
            PlayerPrefs.SetFloat("LastGameScore", 0);
        }
        
        yield return StartCoroutine(BlackIn());
        SceneManager.LoadScene("03_GameResult");
    }

    private IEnumerator StageUpdate()
    {
        HpShame.UpdateValue((int)_char.Hp);
        EnergyShame.UpdateValue(_char.Energy);
        patterRead.CreatePattern(Beat);
        if (_char.Hp < 0)
        {
            //StopCoroutine(BeatUp());
            _char.Death();
            StartCoroutine(GameEnd());
        }
        else
        {
            yield return YieldInstructionCache.WaitFrame;
            StartCoroutine(StageUpdate());
        }
    }

    public void Update(){
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

    public void MoveLeft(){
        gameChar.SetLeft();
    }

    public void MoveRight(){
        gameChar.SetRight();
    }

    public void Jump(){
        gameChar.Jump();
    }

    public void SpecialAbility(){
        gameChar.SpecialAbility();
    }

    public void UnAbility(){
        gameChar.UnSpecialAbility();
    }

}
