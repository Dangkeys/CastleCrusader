using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] AudioClip[] coins;
    [SerializeField] private AudioClip[] boostups;
    [SerializeField] private AudioClip deny;
    [SerializeField] private TextMeshProUGUI displayBalance;

    [SerializeField] private ItemLevelInfo speedInfo;
    [SerializeField] private ItemLevelInfo rangeInfo;
    [SerializeField] private ItemLevelInfo attackInfo;
    [SerializeField] private ItemLevelInfo coinsInfo;
    public ItemLevelInfo SpeedInfo
    {
        get { return speedInfo; }
        set { speedInfo = value; }
    }

    public ItemLevelInfo RangeInfo
    {
        get { return rangeInfo; }
        set { rangeInfo = value; }
    }

    public ItemLevelInfo AttackInfo
    {
        get { return attackInfo; }
        set { attackInfo = value; }
    }

    public ItemLevelInfo CoinsInfo
    {
        get { return coinsInfo; }
        set { coinsInfo = value; }
    }

    private AudioSource audioSource;
    private PlayerStats playerStats;
    [SerializeField] private int currentBalance;
    [SerializeField] int startingBalance = 300;
    [SerializeField] TextMeshProUGUI scoreText;
    int towerCost = 75;
    public int TowerCost => towerCost;
    public int CurrentBalance => currentBalance;
    const float currentBalanceUpdateTime = 10f;
    float runningCurrentBalanceTime = 0f;
    int addCoinAmount = 15;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        playerStats = FindObjectOfType<PlayerStats>();
        audioSource = GetComponent<AudioSource>();
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    private void Update()
    {
        runningCurrentBalanceTime += Time.deltaTime;
        if (runningCurrentBalanceTime >= currentBalanceUpdateTime)
        {
            runningCurrentBalanceTime = 0;
            int randomClip = UnityEngine.Random.Range(0, coins.Length);
            audioSource.PlayOneShot(coins[randomClip], .5f);
            currentBalance += addCoinAmount * coinsInfo.Level;
            UpdateDisplay();
        }
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance = Mathf.Max(0, currentBalance - Mathf.Abs(amount));
        UpdateDisplay();
    }

    public void Buy(ItemLevelInfo itemInfo, Action successAction)
    {
        if (currentBalance >= itemInfo.Cost && itemInfo.Level < itemInfo.Max)
        {
            int randomClip = UnityEngine.Random.Range(0, boostups.Length);
            audioSource.PlayOneShot(boostups[randomClip], .5f);
            Withdraw(itemInfo.Cost);
            itemInfo.IncreaseCost();
            itemInfo.Level++;
            itemInfo.UpdatePriceDisplay();
            itemInfo.UpdateLevelDisplay();
            Debug.Log("Level updated: " + itemInfo.Level);
            successAction?.Invoke();
        }
        else
        {
            audioSource.PlayOneShot(deny, 0.25f);
        }
    }

    public void BuySpeed()
    {
        Buy(speedInfo, playerStats.AddSpeed);
    }

    public void BuyRange()
    {
        Buy(rangeInfo, playerStats.AddRange);
    }

    public void BuyAttack()
    {
        Buy(attackInfo, playerStats.AddAttack);
    }

    public void BuyCoins()
    {
        Buy(coinsInfo, playerStats.AddCoins);
    }

    private void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
        UpdateScoreDisplay();
        speedInfo.UpdatePriceDisplay();
        rangeInfo.UpdatePriceDisplay();
        attackInfo.UpdatePriceDisplay();
        coinsInfo.UpdatePriceDisplay();
        speedInfo.UpdateLevelDisplay();
        rangeInfo.UpdateLevelDisplay();
        attackInfo.UpdateLevelDisplay();
        coinsInfo.UpdateLevelDisplay();
    }
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + scoreKeeper.Score;
    }
}

[Serializable]
public class ItemLevelInfo
{
    public int Cost;
    public int MultiplyCostValue;
    public TextMeshProUGUI PriceText;
    public int Level;
    public int Max;
    public TextMeshProUGUI LevelText;

    public void IncreaseCost()
    {
        Cost *= MultiplyCostValue;
    }

    public void UpdatePriceDisplay()
    {
        PriceText.text = Level < Max ? Cost + "-" : "";
    }

    public void UpdateLevelDisplay()
    {
        LevelText.text = Level < Max ? "Level: " + Level : "Level: Max";
    }
}
