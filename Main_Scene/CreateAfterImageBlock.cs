using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAfterImageBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject afterimage_Block;

    private void Start()
    {
        StartCoroutine(CreateRandomBlock());
    }


    private IEnumerator CreateRandomBlock()
    { 
        while(true){
            for (int i = 0; i < 10; i++)
            {
                float xPos = Random.Range(0.0f, 16.0f) - 8.0f;
                float yPos = Random.Range(0.0f, 9.0f) - 4.0f;

                GameObject block = Instantiate(afterimage_Block, new Vector2(xPos, yPos), Quaternion.identity);
                SpriteRenderer spriteRenderer = block.gameObject.GetComponent<SpriteRenderer>();

                float afters = Random.Range(0.0f, 1.1f);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, afters);
                block.transform.localScale = new Vector2(afters % 0.4f + 0.2f, afters % 0.4f + 0.2f);

                yield return YieldInstructionCache.WaitingSecond(0.01f);
            }
            yield return YieldInstructionCache.WaitingSecond(0.9f);
            StartCoroutine(CreateRandomBlock());
            }
    }

}
