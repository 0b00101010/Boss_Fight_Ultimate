using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float bossDamage; // 보스 공격력
    private float coefficient; // 계수
    private float declineValue = 0; // 감소될 데미지
    private float hitDamage; // 실제로 가해지는 데미지
    private void Start()
    {
        bossDamage = GameObject.FindWithTag("Boss").GetComponent<StageBoss>().Damage;
    }
    
    public float GetDamage(Enemy enemy) {
        coefficient = enemy.Coefficient;
        hitDamage += bossDamage * coefficient;

        if (hitDamage - declineValue > 0)
            return hitDamage - declineValue;

        return 0.0f;
    }

    public void DeclineDamage(float declineValue) {
        this.declineValue = declineValue;
    }

}
