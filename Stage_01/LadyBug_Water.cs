﻿using System.Collections;
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
        yield return new WaitForSeconds(3.5f);
        //for (int i = 0; i < 10; i++)
        //{
        //    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.1f);
        //    yield return new WaitForSeconds(0.05f);
        //}
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer,0.5f));

        spriteRenderer.enabled = false;
        gameObject.transform.tag = "Untagged";

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<Character>().Speed
            = other.gameObject.GetComponent<Character>().MaxSpeed / 100 * 50;
            StartCoroutine(Count(other.gameObject.GetComponent<Character>()));
        }

    }

    private IEnumerator Count(Character character)
    {
        yield return new WaitForSeconds(3.0f);
        character.Speed = character.MaxSpeed;
    }
}
