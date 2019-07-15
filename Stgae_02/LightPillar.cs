﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : Enemy
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject lightPillar;

    private void Start()
    {
        Coefficient = 0.5f;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Excute());

    }

    private IEnumerator Excute()
    {
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer, 0.25f));
        GameObject target = Instantiate(lightPillar, gameObject.transform.position, Quaternion.identity);
        yield return StartCoroutine(GameManager.instance.FadeOut(target.GetComponent<SpriteRenderer>(),0.1f));
        Destroy(target);
        Destroy(this);
    }
}
