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
    // Start is called before the first frame update
    [SerializeField]
    private Canvas settingCanvas;

    [SerializeField]
    private Canvas stagesCanvas;

    [SerializeField]
    private Canvas optionScreenCavans;

    [SerializeField]
    private Slider musicVolumeSlider;

    private void Start()
    {
        StartCoroutine(ImageUpdate());
        StartCoroutine(BlackOut());
        curStageNumber = 0;
        curStage = stages[curStageNumber];
        curStage.gameObject.transform.localScale += new Vector3(.4f, .4f, .4f);
        musicVolumeSlider.value = GameManager.instance.soundManager.Volume;
    }

    private IEnumerator ImageUpdate()
    {
        GameObject target = Instantiate(GameManager.instance.nowGameCharacter,new Vector2(-100f, -100f),Quaternion.identity);
        characterSelectSceneButton.sprite = target.GetComponent<SpriteRenderer>().sprite;
        yield return new WaitForSeconds(0.05f);
        Destroy(target);
    }

    private IEnumerator BlackOut()
    {
        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 6; i++)
        {
            blackspriteRenderer.color = new Color(blackspriteRenderer.color.r, blackspriteRenderer.color.g, blackspriteRenderer.color.b, blackspriteRenderer.color.a - 0.1f);
            yield return new WaitForSeconds(0.02f);
            if (i == 5)
            {
                blackspriteRenderer.color = new Color(blackspriteRenderer.color.r, blackspriteRenderer.color.g, blackspriteRenderer.color.b, 0.0f);
                break;
            }
        }

    }

    public void VolumeChange()
    {
        GameManager.instance.soundManager.Volume = musicVolumeSlider.value;    
    }

    private IEnumerator BlackIn()
    {
        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 10; i++)
        {

            blackspriteRenderer.color = new Color(blackspriteRenderer.color.r, blackspriteRenderer.color.g, blackspriteRenderer.color.b, blackspriteRenderer.color.a + 0.1f);
            yield return new WaitForSeconds(0.03f);
           

        }

    }

    private void Update()
    {
        if (GameManager.instance.touchManager.IsSwiped)
            MoveStageButton();

    }

    private void MoveStageButton()
    {
        if (GameManager.instance.touchManager.SwipeDirection.x < 0 && stages[stages.Count-1].gameObject.transform.position.x > 1.0f)
        {
            foreach (StageButton stage in stages)
            {
                //stage.gameObject.transform.position = new Vector2(stage.gameObject.transform.position.x - 4.0f, stage.gameObject.transform.position.y);
                stage.gameObject.transform.Translate(new Vector2(-4.0f, 0.0f));
            }


            //curStage.gameObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
            StartCoroutine(ButtonSizeUpDown(0, curStage));
            curStageNumber++;
            curStage = stages[curStageNumber];
            GameManager.instance.nowPlayStage = curStage;
            StartCoroutine(ButtonSizeUpDown(1, curStage));
            //curStage.gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (GameManager.instance.touchManager.SwipeDirection.x > 0 && stages[0].gameObject.transform.position.x < -1.0f)
        {
            foreach (StageButton stage in stages)
            {
                //stage.gameObject.transform.position = new Vector2(stage.gameObject.transform.position.x + 4.0f, stage.gameObject.transform.position.y);
                stage.gameObject.transform.Translate(new Vector2(4.0f, 0.0f));
            }

            //curStage.gameObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
            StartCoroutine(ButtonSizeUpDown(0, curStage));
            curStageNumber--;
            curStage = stages[curStageNumber];
            GameManager.instance.nowPlayStage = curStage;
            StartCoroutine(ButtonSizeUpDown(1, curStage));
            //curStage.gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);

        }


    }

    private IEnumerator ButtonSizeUpDown(int sign, StageButton stage)
    {
        for (int i = 0; i < 4; i++)
        {
            if (sign == 0)
            {
                stage.gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                // Debug.Log("Minus!");
            }

            else if (sign == 1)
            {
                stage.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                // Debug.Log("Plus!");
            }

            yield return new WaitForSeconds(0.003f);
        }

        
    }

    public void MoveToInGame(int sceneNumber)
    {
        StartCoroutine(BlackWait(sceneNumber));
    }

    private IEnumerator BlackWait(int sceneNumber)
    {
        yield return StartCoroutine(BlackIn());
        GameManager.instance.NextStageNumber = sceneNumber;
        SceneManager.LoadScene("02_LoadScene");
    }

    public void SettingView()
    {
        settingCanvas.gameObject.SetActive(false);
        stagesCanvas.gameObject.SetActive(false);
        optionScreenCavans.gameObject.SetActive(true);
    }

    public void SettingCancle()
    {
        settingCanvas.gameObject.SetActive(true);
        stagesCanvas.gameObject.SetActive(true);
        optionScreenCavans.gameObject.SetActive(false);
            
    }

    public void OnOffOtherStage()
    {
        StartCoroutine(OnOff());
    }

    private IEnumerator OnOff()
    {
        for (int i = 0; i < stages.Count; i++)
        {
            if (i.Equals(curStageNumber))
                continue;

            if (stages[i].gameObject.active.Equals(true))
            {


                if (stages[i].gameObject.GetComponent<RectTransform>().position.x < stages[curStageNumber].transform.position.x)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        stages[i].gameObject.transform.Translate(new Vector3(-0.3f, 0, 0));
                        yield return new WaitForSeconds(0.02f);
                    }

                }


                else if (stages[i].gameObject.GetComponent<RectTransform>().position.x > stages[curStageNumber].transform.position.x)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        stages[i].gameObject.transform.Translate(new Vector3(+0.3f, 0, 0));
                        yield return new WaitForSeconds(0.02f);
                    }
                }
                stages[i].gameObject.SetActive(!stages[i].gameObject.active);
            }
            else if (stages[i].gameObject.active.Equals(false))
            {
                stages[i].gameObject.SetActive(!stages[i].gameObject.active);
                if (stages[i].gameObject.GetComponent<RectTransform>().position.x > stages[curStageNumber].transform.position.x)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        stages[i].gameObject.transform.Translate(new Vector3(-0.3f, 0, 0));
                        yield return new WaitForSeconds(0.02f);
                    }

                }


                else if (stages[i].gameObject.GetComponent<RectTransform>().position.x < stages[curStageNumber].transform.position.x)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        stages[i].gameObject.transform.Translate(new Vector3(+0.3f, 0, 0));
                        yield return new WaitForSeconds(0.02f);
                    }
                }
            }
            
        }


    }

    public void MoveToCharacterSelect()
    {
        StartCoroutine(BlackWaitToCharacterScene());
    }
    private IEnumerator BlackWaitToCharacterScene()
    {
        yield return StartCoroutine(BlackIn());
        SceneManager.LoadScene("04_CharacterSelect");
    }
}
