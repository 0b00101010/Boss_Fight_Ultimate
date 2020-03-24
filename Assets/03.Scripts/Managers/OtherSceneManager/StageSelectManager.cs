using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StageSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject blackBackground;
    
    [SerializeField]
    private List<StageButton> stages;
    private StageButton curStage;
    private int curStageNumber;

    [SerializeField]
    private Image characterSelectSceneButton;
    
    [SerializeField]
    private Canvas settingCanvas;

    [SerializeField]
    private Canvas stagesCanvas;

    [SerializeField]
    private Canvas optionScreenCavans;

    [SerializeField]
    private Slider musicVolumeSlider;

    private SpriteRenderer blackBackgroundSpriteRenderer;

    private CreateAfterImageBlock createAfterImageBlock;

    private bool isButtonClick = false;

    private void Start(){
        StartCoroutine(ImageUpdate());
        
        blackBackgroundSpriteRenderer = blackBackground.GetComponent<SpriteRenderer>();
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(blackBackgroundSpriteRenderer,1.0f));
        
        createAfterImageBlock = gameObject.GetComponent<CreateAfterImageBlock>();
        
        curStageNumber = 0;
        curStage = stages[curStageNumber];
        curStage.gameObject.transform.localScale += new Vector3(0.4f, 0.4f, 0.4f);
        musicVolumeSlider.value = GameManager.instance.soundManager.Volume;

        createAfterImageBlock.StartCreateAfterImage();
    }

    private IEnumerator ImageUpdate(){
        GameObject gameCharcter = Resources.Load<GameObject>("Characters/" + PlayerPrefs.GetString("SelectCharacter"));
        GameObject target = Instantiate(gameCharcter,new Vector2(-100f, -100f),Quaternion.identity);
        characterSelectSceneButton.sprite = target.GetComponent<SpriteRenderer>().sprite;
        yield return YieldInstructionCache.WaitFrame;
        Destroy(target);
    }

    public void VolumeChange(){
        GameManager.instance.soundManager.Volume = musicVolumeSlider.value;    
    }

    private void Update(){
        if (GameManager.instance.touchManager.IsSwipe){
            MoveStageButton();
        }
    }

    private void MoveStageButton(){
        Vector2 moveVector = Vector2.zero;
        moveVector.x = 4.0f;
        
        if (GameManager.instance.touchManager.SwipeDirection.x < 0 && stages[stages.Count-1].gameObject.transform.position.x > 1.0f){
            foreach (StageButton stage in stages){
                stage.gameObject.transform.Translate(-moveVector);
            }
            StartCoroutine(ButtonSizeUpDown(0, curStage));
            curStageNumber++;
            curStage = stages[curStageNumber];
            StartCoroutine(ButtonSizeUpDown(1, curStage));
        }
        else if (GameManager.instance.touchManager.SwipeDirection.x > 0 && stages[0].gameObject.transform.position.x < -1.0f){
            foreach (StageButton stage in stages){
                stage.gameObject.transform.Translate(moveVector);
            }
            StartCoroutine(ButtonSizeUpDown(0, curStage));
            curStageNumber--;
            curStage = stages[curStageNumber];
            StartCoroutine(ButtonSizeUpDown(1, curStage));
        }
    }

    private IEnumerator ButtonSizeUpDown(int sign, StageButton stage){
        Vector3 changeScale = Vector3.one;
        changeScale /= 10;
        for (int i = 0; i < 4; i++){
            if (sign.Equals(0)){
                stage.gameObject.transform.localScale -= changeScale;
            }
            else if (sign.Equals(1)){
                stage.gameObject.transform.localScale += changeScale;
            }
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    public void MoveToInGame(int sceneNumber){
        StartCoroutine(BlackWait(sceneNumber));
    }

    private IEnumerator BlackWait(int sceneNumber){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackBackground.GetComponent<SpriteRenderer>(),0.5f));
        PlayerPrefs.SetInt("NextStage",sceneNumber);
        SceneManager.LoadScene("02_LoadScene");
    }

    public void SettingView(){
        settingCanvas.gameObject.SetActive(false);
        stagesCanvas.gameObject.SetActive(false);
        optionScreenCavans.gameObject.SetActive(true);
    }

    public void SettingCancle(){
        settingCanvas.gameObject.SetActive(true);
        stagesCanvas.gameObject.SetActive(true);
        optionScreenCavans.gameObject.SetActive(false);
            
    }

    public void OnOffOtherStage(){
        StartCoroutine(MoveOtherButtons());
    }

    private IEnumerator MoveOtherButtons(){
        if(isButtonClick){
            yield break;
        }

        Vector2 moveVector = Vector2.zero;
        moveVector.x = 0.3f;

        isButtonClick = true;

        for (int i = 0; i < stages.Count; i++){
            if (i.Equals(curStageNumber)){
                continue;
            }
            if (stages[i].gameObject.activeInHierarchy.Equals(true)){
                if (stages[i].gameObject.GetComponent<RectTransform>().position.x < stages[curStageNumber].transform.position.x){
                    for (int j = 0; j < 20; j++){
                        stages[i].gameObject.transform.Translate(-moveVector);
                        yield return YieldInstructionCache.WaitingSecond(0.02f);
                    }
                }
                else if (stages[i].gameObject.GetComponent<RectTransform>().position.x > stages[curStageNumber].transform.position.x){
                    for (int j = 0; j < 20; j++){
                        stages[i].gameObject.transform.Translate(moveVector);
                        yield return YieldInstructionCache.WaitingSecond(0.02f);
                    }
                }
                stages[i].gameObject.SetActive(!stages[i].gameObject.activeInHierarchy);
            }
            else if (stages[i].gameObject.activeInHierarchy.Equals(false)){
                stages[i].gameObject.SetActive(!stages[i].gameObject.activeInHierarchy);
                
                if (stages[i].gameObject.GetComponent<RectTransform>().position.x > stages[curStageNumber].transform.position.x){
                    for (int j = 0; j < 20; j++){
                        stages[i].gameObject.transform.Translate(-moveVector);
                        yield return YieldInstructionCache.WaitingSecond(0.02f);
                    }

                }
                else if (stages[i].gameObject.GetComponent<RectTransform>().position.x < stages[curStageNumber].transform.position.x){
                    for (int j = 0; j < 20; j++){
                        stages[i].gameObject.transform.Translate(moveVector);
                        yield return YieldInstructionCache.WaitingSecond(0.02f);
                    }
                }
            }
            
        }

        isButtonClick = false;
    }

    public void MoveToCharacterSelect(){
        StartCoroutine(BlackWaitToCharacterScene());
    }

    private IEnumerator BlackWaitToCharacterScene(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackBackground.GetComponent<SpriteRenderer>(),0.5f));
        createAfterImageBlock.StopCreateAfterImage();
        SceneManager.LoadScene("04_CharacterSelect");
    }
}
