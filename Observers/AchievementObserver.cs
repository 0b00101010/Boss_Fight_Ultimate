using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementObserver : MonoBehaviour, IObserver
{
    public void Renewal(int eventTag)
    {
        switch (eventTag) {
            case (int)GameManager.ObserveTag.GAME_CLEAR:
                if (GameManager.instance.NextStageNumber.Equals((int)GameManager.StageTag.LADYBUG) || GameManager.instance.LastGameScore >= 3000)
                    UnLock(GameManager.AchievementsTag.LADYBUG_SCORE_3000);
                break;
            case (int)GameManager.ObserveTag.CHARACTER_DEATH:
                if (!GameManager.instance.achievementManager.CheckAchievement(GameManager.AchievementsTag.LIFE_IS_BEAUTIFULE))
                    UnLock(GameManager.AchievementsTag.LIFE_IS_BEAUTIFULE);
                break;
            case (int)GameManager.ObserveTag.GAME_END:

                break;
        }

    }

    private void UnLock(GameManager.AchievementsTag eventTag)
    {
        GameManager.instance.achievementManager.UnLockAchievements(eventTag);
    }
}
