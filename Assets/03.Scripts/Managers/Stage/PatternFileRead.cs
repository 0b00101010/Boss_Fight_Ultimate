using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PatternFileRead : MonoBehaviour
{ 
    private List<string> fileStrs = new List<string>();
    private readonly char line = '/';
    private TextAsset file;
    
    [SerializeField]
    private StagePattern stagePattern;
    
    public void ReadFile(string filePath) {
        file = Resources.Load(filePath) as TextAsset;
        string text = file.text;
        string[] strs = text.Split('\n');
        foreach (string str in strs)
            fileStrs.Add(str);
    }

    public void CreatePattern(int nowBeat) {
        foreach (string str in fileStrs)
        {
            if (str.Contains("@"))
            {
                string[] spString = str.Split(line);

                if (int.Parse(spString[1]).Equals(nowBeat))
                {
                    stagePattern.Execute(int.Parse(spString[2]));
                    fileStrs.Remove(fileStrs[0]);
                    break;
                }
            }
        }
    }


}
