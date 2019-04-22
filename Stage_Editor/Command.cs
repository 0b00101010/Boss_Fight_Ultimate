using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : MonoBehaviour
{
    public int beat;
    public int pattern;

    public Command(int beat,int pattern)
    {
        this.beat = beat;
        this.pattern = pattern;
    }
}
