using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BossLadyBug : StageBoss, StagePattern
{

    private List<BossPattern> wildGrowth = new List<BossPattern>();
    private List<BossPattern> bulletSpread = new List<BossPattern>();
    private List<BossPattern> shotBigBullet = new List<BossPattern>();
    private BossPattern greatMigration;


    [SerializeField]
    private Phase secondPhase;

    [SerializeField]
    private GameObject patterns;

    private void Awake(){
        BossPattern[] tempPatterns;

        tempPatterns = patterns.GetComponentsInChildren<WildGrowth>(true);
        
        foreach(BossPattern tempPattern in tempPatterns){
            wildGrowth.Add(tempPattern);
        }

        tempPatterns = patterns.GetComponentsInChildren<BulletSpread>(true);
        
        foreach(BossPattern tempPattern in tempPatterns){
            bulletSpread.Add(tempPattern);
        }

        tempPatterns = patterns.GetComponentsInChildren<ShotBigBullet>(true);

        foreach(BossPattern tempPattern in tempPatterns){
            shotBigBullet.Add(tempPattern);
        }
        Damage = 50.0f;
        greatMigration = gameObject.GetComponent<GreatMigration>();
    }

    public void Execute(int patternNumber){
        switch(patternNumber){
            case 0:
            GetUsePossiblePattern(wildGrowth)?.Execute();
            break;
            
            case 1:
            GetUsePossiblePattern(bulletSpread)?.Execute();
            break;
            
            case 2:
            GetUsePossiblePattern(shotBigBullet)?.Execute();
            break;
            
            case 3:
            greatMigration.Execute();
            break;
            
            default:
            break;
        }
    }

    public void PhaseUp(){
        secondPhase.Execute();
    }

    private BossPattern GetUsePossiblePattern(List<BossPattern> patterns){
        var possiblePattern = 
        from pattern in patterns
        where pattern.gameObject.activeInHierarchy == false
        select pattern;
        return possiblePattern.FirstOrDefault();
    }
}
