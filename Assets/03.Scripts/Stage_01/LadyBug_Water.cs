using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBug_Water : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        float scale = Random.Range(0.5f, 1.3f);
        float aColor = Random.Range(0.4f,1.0f);
        gameObject.transform.localScale = new Vector3(scale,scale);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,aColor);
        StartCoroutine(Excute());
    }

    private IEnumerator Excute()
    {
        yield return YieldInstructionCache.WaitingSecond(3.5f);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.5f));

        spriteRenderer.enabled = false;
        gameObject.transform.tag = "Untagged";
        DestroyImmediate(gameObject.GetComponent<Collider2D>());
        Destroy(this,3.0f); // TODO : Change to  bject pulling
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<Character>().Speed
            = other.gameObject.GetComponent<Character>().MaxSpeed / 100 * 20;
            StartCoroutine(Count(other.gameObject.GetComponent<Character>()));
        }

    }

    private IEnumerator Count(Character character)
    {
        yield return YieldInstructionCache.WaitingSecond(3.0f);
        character.Speed = character.MaxSpeed;
    }
}
