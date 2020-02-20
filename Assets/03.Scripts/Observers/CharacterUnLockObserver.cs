 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnLockObserver : MonoBehaviour
{

    public void Renewal(int eventTag) {
        switch (eventTag)
        {
            case (int)GameManager.AchievementsTag.LIFE_IS_BEAUTIFULE:
                UnLock();
                break;
        }
    }


    private void UnLock() {
    
    }
}
