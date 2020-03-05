using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private int repeatFrame;

    public IEnumerator ImageFadeInCoroutine(Image image, float spendTime){
        Color colorVariable = image.color;
        colorVariable.a = 0;

        for(int i = 0; i <= repeatFrame; i++){
            colorVariable.a = ((float)i/repeatFrame);
            image.color = colorVariable;
            yield return YieldInstructionCache.WaitingSecond(spendTime/repeatFrame);    
        }
    }

    public IEnumerator ImageFadeOutCoroutine(Image image, float spendTime){
        Color colorVariable = image.color;
        colorVariable.a = 1;

        for(int i = 0; i <= repeatFrame; i++){
            colorVariable.a = 1.0f - ((float)i/repeatFrame);
            image.color = colorVariable;
            yield return YieldInstructionCache.WaitingSecond(spendTime/repeatFrame);
        }
    }

    public IEnumerator SpriteFadeInCoroutine(SpriteRenderer spriteRenderer, float spendTime){
        Color colorVariable = spriteRenderer.color;
        colorVariable.a = 0;

        for(int i = 0; i <= repeatFrame; i++){
            colorVariable.a = ((float)i/repeatFrame);
            spriteRenderer.color = colorVariable;
            yield return YieldInstructionCache.WaitingSecond(spendTime/repeatFrame);
        }
    }

    public IEnumerator SpriteFadeOutCoroutine(SpriteRenderer spriteRenderer, float spendTime){
        Color colorVariable = spriteRenderer.color;
        colorVariable.a = 1;

        for(int i = 0; i <= repeatFrame; i++){
            colorVariable.a = 1.0f - ((float)i/repeatFrame);
            spriteRenderer.color = colorVariable;
            yield return YieldInstructionCache.WaitingSecond(spendTime/repeatFrame);
        }
    }
}
