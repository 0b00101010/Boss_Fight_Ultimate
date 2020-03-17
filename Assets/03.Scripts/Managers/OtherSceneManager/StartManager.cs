using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    #region Var
    [SerializeField]
    private Image titleIcon;

    [SerializeField]
    private Image startButton;

    [SerializeField]
    private GameObject blackBackground;

    private float transparency = 1.0f;
    private int transparencySwitch = 1;


    private float titleTransparency = 1.0f;
    #endregion Var

    private void Start()
    {
        StartCoroutine(SetTransparency());
    }

    private void Update()
    {

        titleIcon.color = new Color(titleIcon.color.r, titleIcon.color.g, titleIcon.color.b, titleTransparency);
        startButton.color = new Color(startButton.color.r, startButton.color.g, startButton.color.b, transparency);
        if (GameManager.instance.touchManager.IsTouch || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(StartGame());
        }

    }

    private IEnumerator SetTransparency()
    {
        if (transparencySwitch == 1)
        {
            transparency -= 0.2f;
            titleTransparency -= 0.1f;
        }
        else if (transparencySwitch == 0)
        {
            transparency += 0.2f;
            titleTransparency -= 0.1f;
        }
        yield return YieldInstructionCache.WaitingSecond(0.15f);

        if (transparency < 0.2f) transparencySwitch = 0;
        else if (transparency.Equals(1.0f)) transparencySwitch = 1;

        if (titleTransparency < 0.4f) titleTransparency = 1.0f;
        StartCoroutine(SetTransparency());

    }

    private IEnumerator StartGame()
    {
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(blackBackground.GetComponent<SpriteRenderer>(),0.2f));
        SceneManager.LoadScene("01_Stage_Select");
        
    }


           
}
