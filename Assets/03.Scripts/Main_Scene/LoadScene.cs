using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private Image loadImage;

    [SerializeField]
    private GameObject blackBackground ;

    private void Start()
    {
        loadImage.sprite = GameManager.instance.LoadImages[GameManager.instance.NextStageNumber - 5];
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene()
    {
        StartCoroutine(BlackOut());
        yield return YieldInstructionCache.WaitingSecond(3.0f);

        yield return StartCoroutine(BlackIn());
        SceneManager.LoadScene(GameManager.instance.NextStageNumber);
    }

    private IEnumerator BlackOut()
    {
        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 6; i++)
        {
            blackspriteRenderer.color = new Color(blackspriteRenderer.color.r, blackspriteRenderer.color.g, blackspriteRenderer.color.b, blackspriteRenderer.color.a - 0.1f);
            yield return YieldInstructionCache.WaitFrame;
            if (i == 5)
            {
                blackspriteRenderer.color = new Color(blackspriteRenderer.color.r, blackspriteRenderer.color.g, blackspriteRenderer.color.b, 0.0f);
                break;
            }
        }

    }

    private IEnumerator BlackIn()
    {
        SpriteRenderer blackspriteRenderer = blackBackground.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 10; i++)
        {
            blackspriteRenderer.color = new Color(blackspriteRenderer.color.r, blackspriteRenderer.color.g, blackspriteRenderer.color.b, blackspriteRenderer.color.a + 0.1f);
            yield return YieldInstructionCache.WaitFrame;
        }

    }

}
