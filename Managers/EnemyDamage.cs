using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float bossDamage;
    private float coefficient;
    private void Start()
    {
        bossDamage = GameObject.FindWithTag("Boss").GetComponent<StageBoss>().Damage;
    }
    
    public float GetDamage(Enemy enemy) {
        coefficient = enemy.Coefficient;
        Debug.Log("Damage : " + bossDamage * coefficient);
        return bossDamage * coefficient;
    }
}
