﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEyes : Enemy
{
    [SerializeField]
    private GameObject ball;
    private StageBoss boss;
    private Transform bossTransform;
    private void Start()
    {
        Coefficient = 1.0f;
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss_LadyBug>();
        bossTransform = GameObject.FindWithTag("Boss").GetComponent<Transform>();
        StartCoroutine(Excute());
    }

    private IEnumerator Excute()
    {
        yield return YieldInstructionCache.WaitFrame;
        GameObject eyeBall = Instantiate(ball, bossTransform.position, Quaternion.identity);
        eyeBall.GetComponent<EyeBall>().SetPos(bossTransform.position, gameObject.transform.position);
        Destroy(gameObject, 1.0f);
    }
}
