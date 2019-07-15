using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRazor : Enemy
{
    private void Start()
    {
        Coefficient = 3.0f;
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(new Vector3(0,-1,0));
    }
}
