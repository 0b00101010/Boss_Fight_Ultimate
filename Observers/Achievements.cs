using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour
{
    private bool isUnLock = false;
    private string name;

    public bool IsUnLock { get => isUnLock; set => isUnLock = value; }
    public string Name { get => name; set => name = value; }

    public Achievements(string name)
    {
        this.Name = name;
    }

}