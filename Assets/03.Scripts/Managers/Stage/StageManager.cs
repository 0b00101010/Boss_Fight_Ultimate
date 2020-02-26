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

    
    private AudioSource audioSource;

    [SerializeField]
    private GameObject blackBackground;

    [SerializeField]
    private ValueCtrl BeatValue;

    [SerializeField]
    private ValueCtrl HpValue;
    
    [SerializeField]
    private ValueCtrl EnergyValue;

    private Character _char;

    private string filePath;


    [SerializeField]
    private PatternFileRead patterRead;

    [SerializeField]
    private int musicNumber = 0;
    
    [SerializeField]
    private int filePathNumber = 0;

    public int Beat { get => beat; set => beat = value; }

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
        HpValue.UpdateValue((int)_char.Hp);
        EnergyValue.UpdateValue(_char.Energy);
        patterRead.ReadFile(filePath);
    }

    private IEnumerator BeatUp(){
        Beat++;
        BeatValue.UpdateValue(Beat);

        yield return YieldInstructionCache.WaitingRealTime(beatUpSpeed);
        
        if (beat < lastBeat){
            StartCoroutine(BeatUp());
        }
        else if (beat == lastBeat){
            StartCoroutine(GameEnd());
        }
    }

    private void CreateCharacter(){
        GameObject gameCharacter = Resources.Load<GameObject>("Characters/" + PlayerPrefs.GetString("SelectCharacter"));
        _char = Instantiate(gameCharacter, Vector2.zero,Quaternion.identity).GetComponent<Character>();
        _char.gameObject.transform.tag = "Character";
    }

    private IEnumerator BlackIn(){
        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackspriteRenderer, 1.0f));
        for (int i = 0; i < 20; i++){
            audioSource.spatialBlend += 0.05f;
            yield return YieldInstructionCache.WaitingSecond(0.05f);
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

    private IEnumerator StageUpdate(){
        HpValue.UpdateValue((int)_char.Hp);
        EnergyValue.UpdateValue(_char.Energy);
        patterRead.CreatePattern(Beat);
        if (_char.Hp < 0){
            //StopCoroutine(BeatUp());
            _char.Death();
            StartCoroutine(GameEnd());
        }
        else{
            yield return YieldInstructionCache.WaitFrame;
            StartCoroutine(StageUpdate());
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            _char.SetLeft();
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            _char.SetLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            _char.SetRight();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            _char.SetRight();
        if (Input.GetKeyDown(KeyCode.Z))
            _char.Jump();
        if (Input.GetKeyDown(KeyCode.X))
            _char.SpecialAbility();
        if (Input.GetKeyUp(KeyCode.X))
            _char.UnSpecialAbility();
    }

    public void MoveLeft(){
        _char.SetLeft();
    }

    public void MoveRight(){
        _char.SetRight();
    }

    public void Jump(){
        _char.Jump();
    }

    public void SpecialAbility(){
        _char.SpecialAbility();
    }

    public void UnAbility(){
        _char.UnSpecialAbility();
    }
}
