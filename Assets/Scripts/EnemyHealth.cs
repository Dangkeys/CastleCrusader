using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 3;
    int turretDamage;
    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] float difficultyRamp;
    [SerializeField] float currentHitPoints = 0;
    Enemy enemy;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] AudioClip[] hitSFX;

    [SerializeField] GameObject deathFX;
    PlayerStats playerStats;
    Bank bank;
    ScoreKeeper scoreKeeper;
    int addScoreAmount = 30;
    float totalStats;
    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        bank = FindObjectOfType<Bank>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void SetAttack()
    {
        turretDamage = playerStats.TurretDamage;
        Debug.Log(turretDamage);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
        SetAttack();
    }
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints -= turretDamage;
        if (currentHitPoints <= 0)
        {
            Death();
        }
        else
        {
            //random hitSFX
            Instantiate(hitFX, transform.position, Quaternion.identity);
            int randomClip = UnityEngine.Random.Range(0, hitSFX.Length);
            GetComponent<AudioSource>().PlayOneShot(hitSFX[randomClip], .25f);
        }
    }
    public void Death()
    {
        //I know that my code is written so badly
        Instantiate(deathFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        AddHP();
        enemy.RewardGold();
        scoreKeeper.AddScore(addScoreAmount);
        bank.UpdateScoreDisplay();
        addScoreAmount += 5;
    }
    void AddHP()
    {
        difficultyRamp = Mathf.Ceil((float)playerStats.TurretCounter*1/3);
        totalStats = Mathf.Ceil((float)(bank.SpeedInfo.Level + 
        bank.AttackInfo.Level + 
        bank.CoinsInfo.Level + 
        bank.RangeInfo.Level)*1/2);
        maxHitPoints += difficultyRamp * totalStats;
        // Debug.Log("maxhitpoints: " + maxHitPoints + ", difficultyRamp: " + difficultyRamp + ", totalStats: " + totalStats);
    }
}
