using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreateAfterImageBlock : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> afterImageBlockSpriteRenderers = new List<SpriteRenderer>();

    [SerializeField]
    private List<AfterImageBlock> afterImageBlocks = new List<AfterImageBlock>();

    private IEnumerator createRandomBlockCoroutine;

    private int skipNumber = 0; 
    public void StartCreateAfterImage() {
        createRandomBlockCoroutine = CreateRandomBlock();
        StartCoroutine(createRandomBlockCoroutine);
    }


    [ContextMenu("Bodka")]
    public void StopCreateAfterImage(){
        StopCoroutine(createRandomBlockCoroutine);
    }

    private IEnumerator CreateRandomBlock()
    { 
        while(true){
            for (int i = 0; i < 10; i++)
            {
                if(afterImageBlockSpriteRenderers[i].gameObject.activeInHierarchy){
                    continue;
                }
                afterImageBlockSpriteRenderers[i].gameObject.SetActive(true);

                Color afterColor = Color.white;
                    
                afterColor.a = Random.Range(0.0f, 1.0f);
                    
                Vector2 newScale;
                newScale.x = Random.Range(0.5f, 1.0f);
                newScale.y = newScale.x;
                    
                Vector2 newPosition;
                newPosition.x = Random.Range(-8.0f, 8.0f);
                newPosition.y = Random.Range(-4.0f, 4.0f);
                
                afterImageBlockSpriteRenderers[i].color = afterColor;
                afterImageBlockSpriteRenderers[i].gameObject.transform.localScale = newScale;
                afterImageBlockSpriteRenderers[i].gameObject.transform.position = newPosition;
                    
                StartCoroutine(afterImageBlocks[i].FadeOut());   

                yield return YieldInstructionCache.WaitFrame;
            }
            yield return YieldInstructionCache.WaitingSecond(0.9f);
        }
    }

}
