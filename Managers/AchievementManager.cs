using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private List<Achievements> achievements = new List<Achievements>();
    private TextAsset achievementsFile;
    private string[] achievementsLine;

    public bool CheckAchievement(GameManager.AchievementsTag tag)
    {
        foreach (Achievements achievement in achievements)
        {
            if (achievement.Name.Equals(tag.ToString()))
            {
                return achievement.IsUnLock;
            }
        }
        return false;
    }

    public void UnLockAchievements(GameManager.AchievementsTag tag) {
        foreach(Achievements achievement in achievements)
        {
            if (achievement.Name.Equals(tag.ToString()))
            {
                achievement.IsUnLock = true;
                SaveAchievement();
                break;
            }
        }
    }

    public void LoadAchievement() {
        achievementsFile = Resources.Load("Achievements/Achievements") as TextAsset;
        string text = achievementsFile.text;
        achievementsLine = text.Split('\n');

        foreach (string achievement in achievementsLine) {
            string[] str = achievement.Split(';');
            Achievements newAchi = new Achievements(str[0]);
            newAchi.IsUnLock = false;
            if (str[1].Equals(1))
                newAchi.IsUnLock = true;
            Debug.Log(newAchi);
            achievements.Add(newAchi);
        }
    }

    public void SaveAchievement() {

        if (achievements[0].IsUnLock)
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Achievements/Achievements.txt",achievements[0].Name + ";" + "1");
        else
            System.IO.File.WriteAllText(Application.dataPath + "/Resources/Achievements/Achievements.txt",achievements[0].Name + ";" + "0");


        for (int i = 1; i < achievements.Count; i++)
        {
            Debug.Log(achievements[i].Name);
            Debug.Log(achievements[i].IsUnLock);

            if (achievements[i].IsUnLock)
                System.IO.File.AppendAllText(Application.dataPath + "/Resources/Achievements/Achievements.txt", "\n" + achievements[i].Name + ";" + "1");
            else
                System.IO.File.AppendAllText(Application.dataPath + "/Resources/Achievements/Achievements.txt", "\n" + achievements[i].Name + ";" + "0");
        }
        achievements.Clear();
        LoadAchievement();
    }

}

