﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ChaosFantasy : StageBoss
{

    [SerializeField]
    private GameObject rainbowCircle;

    [SerializeField]
    private GameObject vibrateEye;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        Damage = 100;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Blink();
    }

    public void Blink() {
        StartCoroutine(EyeBlink());
    }

    public void StopBlink() {
        StopCoroutine(EyeBlink());
    }

    private IEnumerator EyeBlink()
    {
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer,0.25f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(spriteRenderer, 0.25f));
        StartCoroutine(EyeBlink());
    }

    public void Vibrate() {
        StartCoroutine(EyeVibrate());
        StartCoroutine(RainBowCircle());
    }

    private IEnumerator EyeVibrate()
    {
        GameObject newEye = Instantiate(vibrateEye, new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        Destroy(newEye, 0.5f);
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(newEye.GetComponent<SpriteRenderer>(), 0.3f));
        yield return YieldInstructionCache.WaitingSecond(0.25f);
        StartCoroutine(EyeVibrate());
    }

    private IEnumerator RainBowCircle()
    {
        GameObject rainbow = Instantiate(rainbowCircle, gameObject.transform.position, Quaternion.identity);

        rainbow.transform.Rotate(new Vector3(0, 0, Random.Range(0f, 90f)));
        yield return GameManager.instance.fadeManager.SpriteFadeOutCoroutine(rainbow.GetComponent<SpriteRenderer>(), 0.5f);
        Destroy(rainbow);
        yield return YieldInstructionCache.WaitingSecond(1.0f);

        StartCoroutine(RainBowCircle());
    }
}
