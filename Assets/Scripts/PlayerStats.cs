using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 0.5f;
    public float Speed { get { return speed; } }
    [SerializeField] int range = 15;
    public int Range { get { return range; } }
    [SerializeField] int turretDamage = 1;
    public int TurretDamage { get { return turretDamage; } }
    [SerializeField] int goldPenalty = 30;
    [SerializeField] int goldReward = 20;
    public int GoldPenalty { get { return goldPenalty; } }
    public int GoldReward { get { return goldReward; } }
    int turretCounter = 0;
    public int TurretCounter { get { return turretCounter; } set { turretCounter = value; } }
    public void AddAttack()
    {
        turretDamage++;
        EnemyHealth[] enemyHealths = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemyHealth in enemyHealths)
        {
            enemyHealth.SetAttack();
        }

    }
    public void AddSpeed()
    {
        speed += 0.25f;
        ControlArrowSpeed[] controlArrowSpeeds = FindObjectsOfType<ControlArrowSpeed>();
        foreach (ControlArrowSpeed controlArrowSpeed in controlArrowSpeeds)
        {
            controlArrowSpeed.SetSpeed();
        }
    }
    public void AddRange()
    {
        range += 10;
        TargetLocator[] targetLocators = FindObjectsOfType<TargetLocator>();
        foreach (TargetLocator targetLocator in targetLocators)
        {
            targetLocator.SetRange();
        }
    }
    public void AddCoins()
    {
        goldReward = Mathf.RoundToInt((float)goldReward * 105/100);
        if (goldReward % 5 != 0)
        {
            goldReward += 5 - (goldReward % 5);
        }
        goldReward += 15; 
        goldPenalty -= 10;
        if (goldPenalty <= 0)
            goldPenalty = 0;
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.SetCoins();
        }
    }
}
